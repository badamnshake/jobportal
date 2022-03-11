using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IJobSeekerUserRepository
    {
        Task CreateJobSeeker(RequestJobSeekerUser request, string appUserEmail);
        Task UpdateJobSeeker(RequestJobSeekerUser request, int jobSeekerUserId);
        Task<JobSeekerUser> GetJobSeeker(string appUserEmail);
        Task<JobSeekerUser> GetJobSeekerDetailsForJobSeeker(int id);
        Task DeleteJobSeeker(JobSeekerUser jobSeekerUser);
    }
}