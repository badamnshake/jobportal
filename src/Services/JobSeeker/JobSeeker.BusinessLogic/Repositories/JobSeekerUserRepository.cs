using System.Linq;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.BusinessLogic.Repositories
{
    public class JobSeekerUserRepository : IJobSeekerUserRepository
    {
        private readonly DataContext _dataContext;

        public JobSeekerUserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateJobSeeker(RequestJobSeekerUser request, string appUserEmail)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spAddJobSeekerUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                request.FirstName,
                request.LastName,
                request.Email,
                appUserEmail,
                request.Phone,
                request.Address,
                request.TotalExperience,
                request.ExpectedSalaryAnnual,
                request.DateOfBirth.ToString()
            );
        }

        public async Task<JobSeekerUser> GetJobSeeker(string appUserEmail)
        {
            var jobSeeker = await _dataContext.JobSeekerUsers.SingleOrDefaultAsync(j => j.AppUserEmail == appUserEmail);
            // var jobSeeker = await GetJobSeekerQuery(appUserEmail)
            // .SingleOrDefaultAsync();
            return jobSeeker;
        }


        public async Task<JobSeekerUser> GetJobSeekerDetailsForEmployer(string appUserEmail)
        {
            var jobSeeker = await GetJobSeekerQuery(appUserEmail)
                .Include(j => j.Qualifications)
                .Include(j => j.Experiences)
                .SingleOrDefaultAsync();
            return jobSeeker;
        }

        public async Task<JobSeekerUser> GetJobSeekerDetailsForJobSeeker(string appUserEmail)
        {
            var jobSeeker = await GetJobSeekerQuery(appUserEmail)
                .Include(j => j.Qualifications)
                .Include(j => j.Experiences)
                .Include(j => j.VacancyRequests)
                .SingleOrDefaultAsync();
            return jobSeeker;
        }

        public async Task UpdateJobSeeker(RequestJobSeekerUser request, int jobSeekerId)
        {
            
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spUpdateJobSeekerUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                jobSeekerId,
                request.FirstName,
                request.LastName,
                request.Phone,
                request.Address,
                request.TotalExperience,
                request.ExpectedSalaryAnnual,
                request.DateOfBirth.ToString()
            );
        }

        public async Task DeleteJobSeeker(int jobSeekerId)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spDeleteJobSeekerUser {0}",
                jobSeekerId
            );
        }


        private IQueryable<JobSeekerUser> GetJobSeekerQuery(string appUserEmail)
        {
            return _dataContext.JobSeekerUsers.FromSqlRaw(
                "exec spGetJobSeekerFromAppUserEmail {0}",
                appUserEmail
            );
        }
    }
}