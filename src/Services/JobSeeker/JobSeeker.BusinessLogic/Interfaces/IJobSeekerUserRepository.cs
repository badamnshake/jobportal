using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IJobSeekerUserRepository
    {
        Task AddJobSeekerUser(RequestJobSeekerUser request);
        Task UpdateJobSeekerUser(RequestJobSeekerUser request);
        Task DeleteJobSeekerUser(RequestAppUserEmail request);
        Task<JobSeekerUser> GetJobSeekerUser(RequestAppUserEmail request);
    }
}