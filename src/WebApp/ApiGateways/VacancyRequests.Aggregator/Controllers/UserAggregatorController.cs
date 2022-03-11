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
    [Route("/api/user")]
    public class UserAggregatorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmployerService _employerService;
        private readonly IJobSeekerService _jobSeekerService;

        public UserAggregatorController(IUserService userService, IEmployerService employerService,
            IJobSeekerService jobSeekerService)
        {
            _userService = userService;
            _employerService = employerService;
            _jobSeekerService = jobSeekerService;
        }


        [Authorize]
        [HttpPost("delete")]
        public async Task<ActionResult> CreateVacancyRequest(RequestCreateVacReq req)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok();
        }
    }
}