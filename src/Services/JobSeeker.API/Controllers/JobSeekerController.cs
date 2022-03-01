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
            await _jobSeekerUserRepository.AddJobSeekerUser(request);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            await _jobSeekerUserRepository.UpdateJobSeekerUser(request);
            return Ok();
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteJobSeekerUser(RequestAppUserEmail appUserEmail)
        {
            await _jobSeekerUserRepository.DeleteJobSeekerUser(appUserEmail);
            return Ok();
        }

    }

}