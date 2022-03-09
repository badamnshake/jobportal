using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/experience")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public ExperienceController(IExperienceRepository experienceRepository, IMapper mapper, IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddQualification(ReqAddExp request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return Forbid("you can't change what you don't own");
            await _experienceRepository.AddExperience(request,jobSeekerId);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteQualification(ReqDelExp request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;

            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(email);
            if (jobSeekerId == default) Forbid("create a profile first");
            if (jobSeekerUser.Qualifications.SingleOrDefault(x => x.Id == request.Id) == null)
            {
                return Forbid("you can't change what you don't own");
            }
            await _experienceRepository.DeleteExperience(request.Id);
            return Ok();
        }
    }
}