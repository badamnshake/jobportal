using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteUser()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.DeleteUser(email);
            await _employerService.DeleteEmployer(email);
            await _jobSeekerService.DeleteJobSeeker(email);
            return Ok();
        }
    }
}