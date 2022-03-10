using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/job-seeker")]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;
        private readonly IMapper _mapper;

        public JobSeekerController(IJobSeekerUserRepository jobSeekerUserRepository, IMapper mapper)
        {
            _jobSeekerUserRepository = jobSeekerUserRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpGet("get")]
        public async Task<ActionResult<ResponseJobSeekerUser>> GetJobSeekerDetailsForJobSeeker()
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            return _mapper.Map<ResponseJobSeekerUser>(jobSeekerUser);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddJobSeekerUser(RequestJobSeekerUser request)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _jobSeekerUserRepository.CreateJobSeeker(request, email);
            return Ok();
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return BadRequest("you can't change what you don't own");
            await _jobSeekerUserRepository.UpdateJobSeeker(request, jobSeekerId);
            return Ok();
        }
    }
}