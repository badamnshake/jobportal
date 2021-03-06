using System.Security.Claims;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JobSeeker.API.Middlewares
{
    /// <summary>
    /// this middleware
    ///  when authorization header is provided it checks if job seeker user from that email exits
    /// if doesn't sets 0 and if exists sets that job seekers users id
    /// controllers use this to quickly get job seeker id
    /// a request from job seeker is always passive
    /// meaning that a job seeker who doesn't own a resource can't change it
    /// job seeker can only change and work with their own attributes
    /// </summary>
    public class VerifyTokenAndAddJobSeekerUserId : IMiddleware
    {
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;

        public VerifyTokenAndAddJobSeekerUserId(IJobSeekerUserRepository jobSeekerUserRepository)
        {
            _jobSeekerUserRepository = jobSeekerUserRepository;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var email = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var jobSeekerUser = await _jobSeekerUserRepository.GetJobSeeker(email);
            context.Items.Add("jobSeekerId", jobSeekerUser == null ? default : jobSeekerUser.Id);
            await next(context);
        }
    }
}