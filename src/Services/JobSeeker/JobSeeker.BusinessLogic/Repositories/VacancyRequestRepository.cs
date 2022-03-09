using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.BusinessLogic.Repositories
{
    public class VacancyRequestRepository : IVacancyRequestRepository
    {
        private readonly DataContext _dataContext;

        public VacancyRequestRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateVacancyRequest(VacancyRequest vacancyRequest)
        {
            _dataContext.VacancyRequests.Add(vacancyRequest);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteVacancyRequest(VacancyRequest vacancyRequest)
        {
            _dataContext.VacancyRequests.Remove(vacancyRequest);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> DoesVacancyRequestExist(int vacancyId, int jobSeekerId)
        {
            var vacReq = await _dataContext.VacancyRequests.SingleOrDefaultAsync(x =>
                x.VacancyId == vacancyId && x.JobSeekerUserId == jobSeekerId);
            return vacReq != null;
        }

        public async Task<VacancyRequest> GetVacancyRequestFromId(int vacancyReqId)
        {
            return await _dataContext.VacancyRequests.FindAsync(vacancyReqId);
        }

        public async Task<List<JobSeekerUser>> GetVacancyRequestJobSeekers(int vacancyId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<int>> GetAppliedVacancies(int jobSeekerId)
        {
            throw new System.NotImplementedException();
        }
    }
}