using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;
using Microsoft.AspNetCore.Authorization;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/experience")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public ExperienceController(IExperienceRepository experienceRepository, IMapper mapper,
            IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }


        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create")]
        public async Task<ActionResult> AddQualification(ReqAddExp request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return BadRequest("please create a profile first | you don't own the resource");
            await _experienceRepository.AddExperience(request, jobSeekerId);
            return Ok();
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteQualification(int id)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;

            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            if (jobSeekerId == default) BadRequest("create a profile first");
            if (jobSeekerUser.Qualifications.SingleOrDefault(x => x.Id == id) == null)
            {
                return Forbid("you can't change what you don't own");
            }

            await _experienceRepository.DeleteExperience(id);
            return Ok();
        }
    }
}