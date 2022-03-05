using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Employer.API.Extensions;
using Employer.BusinessLogic.Interfaces;
using Employer.Infrastructure.Helpers;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels.Vacancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employer.API.Controllers
{
    [ApiController]
    [Route("/api/vacancy")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;

        public VacancyController(IVacancyRepository vacancyRepository, IMapper mapper,
            IEmployerRepository employerRepository)
        {
            _mapper = mapper;
            _vacancyRepository = vacancyRepository;
            _employerRepository = employerRepository;
        }

        [HttpGet("get-details")]
        public async Task<ActionResult<VacancyReponseDetailsDto>> GetDetails([FromQuery] int id)
        {
            var vacancy = await _vacancyRepository.GetVacancyDetails(id);
            if (vacancy == null)
                return BadRequest();
            return _mapper.Map<VacancyReponseDetailsDto>(vacancy);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("create-details")]
        public async Task<ActionResult<VacancyReponseDetailsDto>> CreateDetails(VacancyDetailsDto details)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerRepository.GetEmployer(email);
            
            if (employer == null)
                return BadRequest("login as employer");

            var vac = _mapper.Map<Vacancy>(details);
            vac.PublishedDate = DateTime.Now;
            vac.EmployerEntityId = employer.Id;
            var vacancy = await _vacancyRepository.AddVacancy(vac);
            return _mapper.Map<VacancyReponseDetailsDto>(vacancy);
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("update-details")]
        public async Task<ActionResult> UpdateDetails(VacancyUpdateDto details)
        {
            var vacancy = await VerifyTheTokenHolderAndFindVacancy(details.Id);
            if (vacancy == null) return BadRequest(" the vacancy doesn't exist");

            await _vacancyRepository.UpdateVacancy(vacancy);
            return Ok();
        }

        [Authorize(Roles = "Employer")]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteVacancy(VacancyDeleteDto details)
        {
            var vacancy = await VerifyTheTokenHolderAndFindVacancy(details.Id);
            if (vacancy == null) return BadRequest(" the vacancy doesn't exist");
            await _vacancyRepository.DeleteVacancy(vacancy);
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Vacancy> VerifyTheTokenHolderAndFindVacancy(int id)
        {
            // here when updating details or deleting
            // it is needed to verify that the owner of the token is the owner of the
            // vacancy that's why instead of getting vacancies from vacancy table we go other way around
            // even if vacancy exists but token holder isn't the owner of the vacancy he cant change
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var emp = await _employerRepository.GetEmployer(email);
            var vacancy = emp.Vacancies.SingleOrDefault(vac => vac.Id == id);
            return vacancy;
        }

        [HttpGet("get-vacancies/date")]
        public async Task<ActionResult<PagedList<VacancyReponseDetailsDto>>> GetVacanciesFromDate(
            [FromQuery] DateTime lastDate,
            [FromQuery] DateTime publishedDate,
            [FromQuery] bool fromLastDate,
            PageParams pageParams
        )
        {
            if (fromLastDate)
            {
                var vacancies = await _vacancyRepository.GetVacanciesFromLastDate(lastDate, pageParams);
                AddPaginationHeaderFromPagedList(vacancies);
                return vacancies;
            }
            else
            {
                var vacancies = await _vacancyRepository.GetVacanciesFromPublishedDate(publishedDate, pageParams);
                AddPaginationHeaderFromPagedList(vacancies);
                return vacancies;
            }
        }

        [HttpGet("get-vacancies/orgName")]
        public async Task<ActionResult<PagedList<VacancyReponseDetailsDto>>> GetVacanciesOrganizationName(
            [FromQuery] string organizationName,
            PageParams pageParams
        )
        {
            var vacancies = await _vacancyRepository.GetVacanciesFromOrganization(organizationName, pageParams);
            AddPaginationHeaderFromPagedList(vacancies);
            return vacancies;
        }

        [HttpGet("get-vacancies/location")]
        public async Task<ActionResult<PagedList<VacancyReponseDetailsDto>>> GetVacanciesLocation(
            [FromQuery] string location,
            PageParams pageParams
        )
        {
            var vacancies = await _vacancyRepository.GetVacanciesFromLocation(location, pageParams);
            AddPaginationHeaderFromPagedList(vacancies);
            return vacancies;
        }

        [HttpGet("get-vacancies/salary")]
        public async Task<ActionResult<PagedList<VacancyReponseDetailsDto>>> GetVacanciesSalary(
            [FromQuery] int minSalary,
            PageParams pageParams
        )
        {
            var vacancies = await _vacancyRepository.GetVacanciesFromSalary(minSalary, pageParams);
            AddPaginationHeaderFromPagedList(vacancies);
            return vacancies;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddPaginationHeaderFromPagedList(PagedList<VacancyReponseDetailsDto> vacancies)
        {
            Response.AddPaginationHeader(vacancies.CurrentPage, vacancies.PageSize, vacancies.TotalCount,
                vacancies.TotalPages);
        }
    }
}