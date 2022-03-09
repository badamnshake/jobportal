using System.Threading.Tasks;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IExperienceRepository
    {
        Task AddExperience(ReqAddExp request, int jsId);
        Task DeleteExperience( int eId);
    }
}