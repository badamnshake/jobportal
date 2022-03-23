using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Helpers;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.VacancyRequest;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.BusinessLogic.Repositories
{
    public class VacancyRequestRepository : IVacancyRequestRepository
    {
        // this class has pretty self explanatory code
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public VacancyRequestRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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

        public async Task<VacancyRequest> GetVacancyRequestFromVacancyId(int vacancyReqId, int jsId)
        {
            return await _dataContext.VacancyRequests.SingleOrDefaultAsync(x =>
                x.VacancyId == vacancyReqId && x.JobSeekerUserId == jsId);
        }

        public async Task<PagedList<ResponseJobSeekerUser>> GetJobSeekersOnAVacancy(
            PageParams pageParams, int vacancyId)
        {
            var query = _dataContext.VacancyRequests
                .Where(v => v.VacancyId == vacancyId)
                .Select(j => j.JobSeekerUser)
                .OrderBy(j => j.Id)
                .ProjectTo<ResponseJobSeekerUser>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            // .ToListAsync();
            return await PagedList<ResponseJobSeekerUser>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<PagedList<int>> GetVacanciesWhereJsAppliedPaged(PageParams pageParams, int jobSeekerId)
        {
            var queryable = _dataContext.VacancyRequests.Where(v => v.JobSeekerUserId == jobSeekerId)
                .OrderBy(j => j.VacancyId).Select(v => v.VacancyId);
            return await PagedList<int>.CreateAsync(queryable, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<List<int>> GetVacanciesWhereJsAppliedAll(int jobSeekerId)
        {
            return await _dataContext.VacancyRequests.Where(v => v.JobSeekerUserId == jobSeekerId)
                .OrderBy(j => j.VacancyId).Select(v => v.VacancyId).ToListAsync();
        }
    }
}