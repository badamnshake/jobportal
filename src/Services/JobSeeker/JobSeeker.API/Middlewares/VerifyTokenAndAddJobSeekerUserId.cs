using System.Security.Claims;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JobSeeker.API.Middlewares
{
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