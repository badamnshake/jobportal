using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;
using Microsoft.AspNetCore.Authorization;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/qualification")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public QualificationController(IQualificationRepository qualificationRepository,
            IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _qualificationRepository = qualificationRepository;
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }

        /// <summary>
        ///  adds a qualification to job seeker profile
        /// </summary>
        /// <param name="request">qualification info</param>
        /// <returns> response as 200 or 400 </returns>
        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create")]
        public async Task<ActionResult> AddQualification(ReqAddQualification request)
        {
            // the middleware gets job seeker Id from the auth jwt token and adds to context
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return BadRequest("create a profile first");
            await _qualificationRepository.CreateQualification(request, jobSeekerId);
            return Ok();
        }

        /// <summary>
        ///  deletes a qualification from job seeker profile
        /// </summary>
        /// <param name="id">qualification id</param>
        /// <returns> response as 200 or 400 </returns>
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteQualification(int id)
        {
            // the middleware gets job seeker Id from the auth jwt token and adds to context
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;

            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            if (jobSeekerId == default) BadRequest("create a profile first");


            // it goes through job seeker qualifications and checks if they own it
            if (jobSeekerUser.Qualifications.SingleOrDefault(x => x.Id == id) == null)
            {
                // here it means a job seeker who doesn't own a qualification is trying to change it
                // weird case but taken care of
                return BadRequest("you can't change what you don't own");
            }

            await _qualificationRepository.DeleteQualification(id);
            return Ok();
        }
    }
}