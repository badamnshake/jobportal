using System.Threading.Tasks;
using JobSeeker.API.DTOs;
using JobSeeker.API.Models;

namespace JobSeeker.API.Interfaces
{
    public interface IJobSeekerUserRepository 
    {
        Task<bool> AddJobSeekerUser(RequestJobSeekerUser request);
        Task<bool> UpdateJobSeekerUser(RequestJobSeekerUser request);
        Task<bool> DeleteJobSeekerUser(RequestAppUserEmail request);
        Task<JobSeekerUser> GetJobSeekerUser(RequestAppUserEmail request);

    }
}