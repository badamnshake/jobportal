using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IQualificationRepository
    {
        Task CreateQualification(ReqAddQualification request);
        Task DeleteQualification(ReqDelQualification request);
        Task<IEnumerable<Qualification>> GetQualificationsOfJobSeeker(RequestJobSeekerId request);
    }
}