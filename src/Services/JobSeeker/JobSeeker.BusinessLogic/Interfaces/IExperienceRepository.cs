using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IExperienceRepository
    {
        Task AddExperience(ReqAddExp request);
        Task DeleteExperience(ReqDelExp request);
        Task<IEnumerable<Experience>> GetExperiencesOfJobSeeker(RequestJobSeekerId request);
    }
}