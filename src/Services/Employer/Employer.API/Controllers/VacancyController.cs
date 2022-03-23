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

        [Authorize(Roles = "Employer")]
        [HttpGet("get-vacancies-posted-by-me/")]
        public async Task<ActionResult<PagedList<ResponseVacancyDetails>>> GetVacanciesPostedByMe(
            [FromQuery] PageParams pageParams
        )
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer Doesn't exist please create your employer profile first");

            var empEntity = await _employerRepository.GetEmployer(email);
            var vacancies = await _vacancyRepository.GetVacanciesPostedByMe(pageParams, empEntity.Id);
            // this method adds pagination for the client to use
            AddPaginationHeaderFromPagedList(vacancies);
            return vacancies;
        }

        // get details of a single vacancy
        //
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<ResponseVacancyDetails>> GetDetails(int id)
        {
            // if not found return
            var vacancy = await _vacancyRepository.GetVacancyDetails(id);
            if (vacancy == null)
                return BadRequest();
            return _mapper.Map<ResponseVacancyDetails>(vacancy);
        }

        // employer can create a vacancy
        // it fetches email from the token and creates an associated vacancy with that employer
        // token is used get employer to attach vacancy
        // this means that no other employer can create vacancy for other employer
        [Authorize(Roles = "Employer")]
        [HttpPost("create")]
        public async Task<ActionResult<ResponseVacancyDetails>> CreateDetails(RequestVacancyDetails details)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerRepository.GetEmployer(email);

            // if employer profile not found employer needs to create an employer profile
            // to create employer controller create method is used
            if (employer == null)
                return BadRequest("create a profile first");

            var vac = _mapper.Map<Vacancy>(details);

            // published time is set to now
            // vac.PublishedDate = DateTime.Now;

            // foreign key to emp table
            vac.EmployerEntityId = employer.Id;
            var vacancy = await _vacancyRepository.AddVacancy(vac);
            return _mapper.Map<ResponseVacancyDetails>(vacancy);
        }

        // update vacancy details for employer
        [Authorize(Roles = "Employer")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateDetails(RequestVacancyUpdate details)
        {
            // verify token holder checks if the employer who is trying to update the vacancy owns it or not
            // if only employer owns the vacancy then only he can change
            var vacancy = await VerifyTheTokenHolderAndFindVacancy(details.Id);
            if (vacancy == null) return BadRequest("Either the vacancy doesn't exist or you don't own it");
            _mapper.Map(details, vacancy);
            await _vacancyRepository.UpdateVacancy(vacancy);
            return Ok();
        }

        [Authorize(Roles = "Employer")]
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteVacancy(int id)
        {
            // verify token holder checks if the employer who is trying to update the vacancy owns it or not
            // if only employer owns the vacancy then only he can change
            var vacancy = await VerifyTheTokenHolderAndFindVacancy(id);
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


        // paged list is used as response
        // pagination parameters are required to fetch get vacancies
        // various filtration can be found in vacancy repository
        [HttpGet("get-vacancies/")]
        public async Task<ActionResult<PagedList<ResponseVacancyDetails>>> GetVacancies(
            [FromQuery] VacancyParams vacancyParams
        )
        {
            var vacancies = await _vacancyRepository.GetVacancies(vacancyParams);
            // this method adds pagination for the client to use
            AddPaginationHeaderFromPagedList(vacancies);
            return vacancies;
        }


        // this method adds pagination for the client to use
        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddPaginationHeaderFromPagedList(PagedList<ResponseVacancyDetails> vacancies)
        {
            Response.AddPaginationHeader(vacancies.CurrentPage, vacancies.PageSize, vacancies.TotalCount,
                vacancies.TotalPages);
        }
    }
}