using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;
using Microsoft.AspNetCore.Authorization;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/qualification")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IMapper _mapper;
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public QualificationController(IQualificationRepository qualificationRepository, IMapper mapper,
            IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _qualificationRepository = qualificationRepository;
            _mapper = mapper;
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost("add")]
        public async Task<ActionResult> AddQualification(ReqAddQualification request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return Unauthorized("you can't change what you don't own");
            await _qualificationRepository.CreateQualification(request,jobSeekerId);
            return Ok();
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteQualification(ReqDelQualification request)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;

            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            if (jobSeekerId == default) Forbid("create a profile first");
            if (jobSeekerUser.Qualifications.SingleOrDefault(x => x.Id == request.Id) == null)
            {
                return Forbid("you can't change what you don't own");
            }

            await _qualificationRepository.DeleteQualification(request.Id);
            return Ok();
        }
    }
}