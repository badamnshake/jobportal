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
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public ExperienceController(IExperienceRepository experienceRepository,
            IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _experienceRepository = experienceRepository;
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }


        /// <summary>
        ///  adds a experience to job seeker profile
        /// </summary>
        /// <param name="request">Info about xp</param>
        /// <returns> response as 200 or 400 </returns>
        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create")]
        public async Task<ActionResult> AddExperience(ReqAddExp request)
        {
            // the middleware gets job seeker Id from the auth jwt token and adds to context
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == default) return BadRequest("please create a profile first | you don't own the resource");
            await _experienceRepository.AddExperience(request, jobSeekerId);
            return Ok();
        }

        /// <summary>
        /// Deletes a exp info from job seekers profile
        /// </summary>
        /// <param name="id"> the id of the experience</param>
        /// <returns> status code 200 or 400</returns>
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteExperience(int id)
        {
            // the middleware gets job seeker Id from the auth jwt token
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;

            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeekerDetailsForJobSeeker(jobSeekerId);
            if (jobSeekerId == default) BadRequest("create a profile first");


            // it goes through job seeker xp and checks if they own it
            if (jobSeekerUser.Qualifications.SingleOrDefault(x => x.Id == id) == null)
            {
                // here it means a job seeker who doesn't own a xp is trying to change it
                // weird case but taken care of
                return BadRequest("you can't change what you don't own");
            }

            await _experienceRepository.DeleteExperience(id);
            return Ok();
        }
    }
}