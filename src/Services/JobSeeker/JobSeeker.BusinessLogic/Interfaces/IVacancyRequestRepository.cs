using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Helpers;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.VacancyRequest;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IVacancyRequestRepository
    {
        Task CreateVacancyRequest(VacancyRequest vacancyRequest);
        Task DeleteVacancyRequest(VacancyRequest vacancyRequest);
        Task<bool> DoesVacancyRequestExist(int vacancyId, int jobSeekerId);
        Task<VacancyRequest> GetVacancyRequestFromVacancyId(int vacancyReqId, int jsId);
        Task<PagedList<ResponseJobSeekerUser>> GetJobSeekersOnAVacancy(PageParams pageParams, int vacancyId);
        Task<PagedList<int>> GetVacanciesWhereJsAppliedPaged(PageParams pageParams, int jobSeekerId);
        Task<List<int>> GetVacanciesWhereJsAppliedAll(int jobSeekerId);
    }
}