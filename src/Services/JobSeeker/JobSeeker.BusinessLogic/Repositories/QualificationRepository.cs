using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.BusinessLogic.Repositories
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly DataContext _dataContext;

        public QualificationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddQualification(ReqAddQualification request)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spAddQualification {0}, {1}, {2}, {3}, {4}",
                request.JobSeekerUserId,
                request.QualificationName,
                request.University,
                request.DateOfCompletion,
                request.GradeOrScore
            );
        }

        public async Task DeleteQualification(ReqDelQualification request)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spDeleteQualification {0}",
                request.Id
            );
        }

        public async Task<IEnumerable<Qualification>> GetQualificationsOfJobSeeker(RequestJobSeekerId request)
        {
            var jobSeeker = await _dataContext.JobSeekerUsers
                .Include(u => u.Qualifications)
                .SingleOrDefaultAsync(u => u.Id == request.Id);
            return jobSeeker.Qualifications;
        }
    }
}