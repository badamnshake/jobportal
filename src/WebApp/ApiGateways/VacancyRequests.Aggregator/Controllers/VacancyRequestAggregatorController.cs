using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacancyRequests.Aggregator.Models.Requests;
using VacancyRequests.Aggregator.Models.Responses;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Controllers
{
    [ApiController]
    [Route("/api/vac-req-aggregator")]
    public class VacancyRequestController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IVacancyRequestService _vacancyRequestService;
        private readonly IEmployerService _employerService;

        public VacancyRequestController(IVacancyService vacancyService, IVacancyRequestService vacancyRequestService,
            IEmployerService employerService)
        {
            _vacancyService = vacancyService;
            _vacancyRequestService = vacancyRequestService;
            _employerService = employerService;
        }

        [Authorize(Roles = "Employer")]
        [HttpGet("get-job-seekers-who-applied-on-vacancy/{vacancyId:int}")]
        public async Task<ActionResult<List<ResponseJobSeeker>>> GetJobSeekersAppliedOnVacancy(int vacancyId)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _employerService.GetDetails(email);
            if (!response.IsSuccessStatusCode)
                return BadRequest("Please create an Employer profile first");

            var emp = await response
                .Content.ReadFromJsonAsync<ResponseEmployerDetails>();

            var x = emp!.Vacancies.SingleOrDefault(v => v.Id == vacancyId);
            if (x == null)
                return BadRequest("Either Vacancy doesn't exist or you don't own the vacancy");

            var jobSeekerResponse = await _vacancyRequestService.GetAppliedJobSeekersOnAVacancy(vacancyId);

            if (!response.IsSuccessStatusCode)
                return StatusCode(500);

            return await jobSeekerResponse.Content.ReadFromJsonAsync<List<ResponseJobSeeker>>();
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create/{vacancyId:int}")]
        public async Task<ActionResult> CreateVacancyRequest( int vacancyId)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _vacancyService.GetVacancy(vacancyId);
            var vacancy = await response
                .Content.ReadFromJsonAsync<ResponseVacancyDetails>();
            if (vacancy == null)
                return BadRequest("Vacancy you are trying to apply doesn't exist");
            var request = new RequestCreateVacancyRequest
            {
                vacancyId = vacancyId,
                jobSeekerEmail = email
            };

            var vacReqCreated = await _vacancyRequestService.CreateVacancyRequest(vacancyId, request);

            if (!vacReqCreated.IsSuccessStatusCode)
                return BadRequest("Either you applied already or JobSeeker profile doesn't exist");

            return Ok();
        }
    }
}