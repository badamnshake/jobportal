using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IJobSeekerUserRepository
    {
        Task CreateJobSeekerUser(RequestJobSeekerUser request);
        Task UpdateJobSeekerUser(RequestJobSeekerUser request);
        Task DeleteJobSeekerUser(string appUserEmail);
        Task<JobSeekerUser> GetJobSeekerUser(string appUserEmail);
    }
}