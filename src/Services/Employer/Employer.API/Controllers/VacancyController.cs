using System;
using System.Threading.Tasks;
using AutoMapper;
using Employer.API.Extensions;
using Employer.BusinessLogic.Interfaces;
using Employer.Infrastructure.Helpers;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels.Vacancy;
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

        public VacancyController(IVacancyRepository vacancyRepository, IMapper mapper, IEmployerRepository employerRepository)
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

        [HttpPost("create-details")]
        public async Task<ActionResult<VacancyReponseDetailsDto>> CreateDetails(VacancyDetailsDto details)
        {
            if (await _employerRepository.DoesEmployerExistById(details.EmployerEntityId))
            {
                var vac = _mapper.Map<Vacancy>(details);
                vac.PublishedDate = DateTime.Now;
                var vacancy = await _vacancyRepository.AddVacancy(vac);
                return _mapper.Map<VacancyReponseDetailsDto>(vacancy);
                
            }

            return BadRequest("login as employer");
        }

        [HttpPut("update-details")]
        public async Task<ActionResult> UpdateDetails(VacancyUpdateDto details)
        {
            var vacancy = await _vacancyRepository.GetVacancyDetails(details.Id);
            if (vacancy == null) return BadRequest(" the vacancy doesn't exist");
            await _vacancyRepository.UpdateVacancy(vacancy);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteVacancy(VacancyDeleteDto details)
        {
            var vacancy = await _vacancyRepository.GetVacancyDetails(details.Id);
            if (vacancy == null) return BadRequest(" the vacancy doesn't exist");
            await _vacancyRepository.DeleteVacancy(vacancy);
            return Ok();
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