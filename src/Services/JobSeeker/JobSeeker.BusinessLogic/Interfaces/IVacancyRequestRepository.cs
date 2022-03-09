using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.Infrastrucure.Models;

namespace JobSeeker.BusinessLogic.Interfaces
{
    public interface IVacancyRequestRepository
    {
        Task CreateVacancyRequest(VacancyRequest vacancyRequest);
        Task DeleteVacancyRequest(VacancyRequest vacancyRequest);
        Task<bool> DoesVacancyRequestExist(int vacancyId, int jobSeekerId);
        Task<VacancyRequest> GetVacancyRequestFromId(int vacancyReqId);
        Task<List<JobSeekerUser>> GetVacancyRequestJobSeekers(int vacancyId);
        Task<List<int>> GetAppliedVacancies(int jobSeekerId);
    }
}