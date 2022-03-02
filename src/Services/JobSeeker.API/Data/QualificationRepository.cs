
using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.API.DTOs;
using JobSeeker.API.Interfaces;
using JobSeeker.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.API.Data
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