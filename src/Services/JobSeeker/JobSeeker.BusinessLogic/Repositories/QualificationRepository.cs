using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
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

        public async Task CreateQualification(ReqAddQualification request, int jsId)
        {
            await  _dataContext.Database.ExecuteSqlRawAsync(
            // var query = _dataContext.Qualifications.FromSqlRaw(
                "exec spAddQualification {0}, {1}, {2}, {3}, {4}",
                jsId,
                request.QualificationName,
                request.University,
                request.DateOfCompletion,
                request.GradeOrScore
            );
           // await query.ToListAsync();
           
            // await _dataContext.Database.ExecuteSqlRawAsync(
           
            //     "exec spAddQualification {0}, {1}, {2}, {3}, {4}",
            //     request.JobSeekerUserId,
            //     request.QualificationName,
            //     request.University,
            //     request.DateOfCompletion,
            //     request.GradeOrScore
            // );
        }

        public async Task DeleteQualification( int qId)
        {
            await  _dataContext.Database.ExecuteSqlRawAsync(
            // var query = _dataContext.Qualifications.FromSqlRaw(
                "exec spDeleteQualification {0}",
                qId
            );
            // await query.ToListAsync();
        }

    }
}