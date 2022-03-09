using System.Threading.Tasks;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IQualificationRepository
    {
        Task CreateQualification(ReqAddQualification request, int jsId);
        Task DeleteQualification( int qId);
    }
}