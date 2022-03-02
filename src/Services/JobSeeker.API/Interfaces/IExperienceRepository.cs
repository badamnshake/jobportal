using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.API.DTOs;
using JobSeeker.API.Models;

namespace JobSeeker.API.Interfaces
{
    public interface IExperienceRepository
    {
        Task AddExperience(ReqAddExp request);
        Task DeleteExperience(ReqDelExp request);
        Task<IEnumerable<Experience>> GetExperiencesOfJobSeeker(RequestJobSeekerId request);
    }
}