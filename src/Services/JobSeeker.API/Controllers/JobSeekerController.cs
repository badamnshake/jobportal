using JobSeeker.API.Interfaces;
using JobSeeker.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

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
        public async Task<ActionResult<ResponseJobSeekerUser>> GetJobSeekerUser(RequestAppUserEmail appUserEmail)
        {
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerUser(appUserEmail);
            if (jobSeekerUser == null) return BadRequest("user with email doens't exist");
            return _mapper.Map<ResponseJobSeekerUser>(jobSeekerUser);
        }
        [HttpPost("add")]
        public async Task<ActionResult> AddJobSeekerUser(RequestJobSeekerUser request)
        {
            var success = await _jobSeekerUserRepository.AddJobSeekerUser(request);
            if (success) return Ok();
            else return StatusCode(500, "Internal server error");
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            var success = await _jobSeekerUserRepository.UpdateJobSeekerUser(request);
            if (success) return Ok();
            else return StatusCode(500, "Internal server error");
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteJobSeekerUser(RequestAppUserEmail appUserEmail)
        {
            var success = await _jobSeekerUserRepository.DeleteJobSeekerUser(appUserEmail);
            if (success) return Ok();
            else return StatusCode(500, "Internal server error");
        }

    }

}