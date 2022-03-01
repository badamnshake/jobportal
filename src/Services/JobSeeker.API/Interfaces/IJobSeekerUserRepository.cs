using System.Threading.Tasks;
using JobSeeker.API.DTOs;
using JobSeeker.API.Models;

namespace JobSeeker.API.Interfaces
{
    public interface IJobSeekerUserRepository 
    {
        Task AddJobSeekerUser(RequestJobSeekerUser request);
        Task UpdateJobSeekerUser(RequestJobSeekerUser request);
        Task DeleteJobSeekerUser(RequestAppUserEmail request);
        Task<JobSeekerUser> GetJobSeekerUser(RequestAppUserEmail request);

    }
}