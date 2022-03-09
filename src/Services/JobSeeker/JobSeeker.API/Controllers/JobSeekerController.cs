using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/jobseeker")]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;
        private readonly IMapper _mapper;

        public JobSeekerController(IJobSeekerUserRepository jobSeekerUserRepository, IMapper mapper)
        {
            _jobSeekerUserRepository = jobSeekerUserRepository;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<ResponseJobSeekerUser>> GetJobSeekerUser([FromQuery] string appUserEmail)
        {
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeeker(appUserEmail);
            if (jobSeekerUser == null) return BadRequest("user with email doesn't exist");
            return _mapper.Map<ResponseJobSeekerUser>(jobSeekerUser);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddJobSeekerUser(RequestJobSeekerUser request)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _jobSeekerUserRepository.CreateJobSeeker(request, email);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return Forbid("you can't change what you don't own");
            await _jobSeekerUserRepository.UpdateJobSeeker(request, jobSeekerId);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteJobSeekerUser()
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return Forbid("you can't change what you don't own");
            await _jobSeekerUserRepository.DeleteJobSeeker(jobSeekerId);
            return Ok();
        }
    }
}