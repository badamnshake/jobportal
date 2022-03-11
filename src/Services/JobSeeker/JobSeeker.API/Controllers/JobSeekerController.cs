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

        // here job seeker can get his profile 
        [Authorize(Roles = "JobSeeker")]
        [HttpGet("get")]
        public async Task<ActionResult<ResponseJobSeekerUser>> GetJobSeekerDetailsForJobSeeker()
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            // here job seeker can get his profile and view the applied vacancy Id's 
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            // automapper
            return _mapper.Map<ResponseJobSeekerUser>(jobSeekerUser);
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create")]
        public async Task<ActionResult> AddJobSeekerUser(RequestJobSeekerUser request)
        {
            // gets teh email from the token
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _jobSeekerUserRepository.CreateJobSeeker(request, email);
            return Ok();
        }

        /// <summary>
        /// updates the job seeker user
        /// </summary>
        /// <param name="request">info about job seeker</param>
        /// <returns>status code 200 or 400</returns>
        [Authorize(Roles = "JobSeeker")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            // the middleware gets job seeker Id from the auth jwt token and adds to context
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            // if job seeker is not found then they have to create a profile to update it
            if (jobSeekerId == default) return BadRequest("you need to create a profile first to update it");
            await _jobSeekerUserRepository.UpdateJobSeeker(request, jobSeekerId);
            return Ok();
        }

        /// <summary>
        /// deletes the job seeker
        /// only used from aggregator not directly from ocelot
        /// when a identity user is deleted this is automatically run from aggregator
        /// </summary>
        /// <param name="request"> id of job seeker</param>
        /// <returns> status code 200 or 500</returns>
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteJobSeekerUser(RequestJobSeekerDelete request)
        {
            // the middleware gets job seeker Id from the auth jwt token and adds to context
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeeker(request.Email);
            // if job seeker doesn't exist then there is no reason to delete it
            if (jobSeekerUser == null) return Ok();
            await _jobSeekerUserRepository.DeleteJobSeeker(jobSeekerUser);
            return Ok();
        }
    }
}