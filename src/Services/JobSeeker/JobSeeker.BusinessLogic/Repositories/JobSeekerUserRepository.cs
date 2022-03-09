using System.Globalization;
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

        public async Task CreateJobSeekerUser(RequestJobSeekerUser request)
        {
            var result = await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spAddJobSeekerUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                request.FirstName,
                request.LastName,
                request.Email,
                request.AppUserEmail,
                request.Phone,
                request.Address,
                request.TotalExperience,
                request.ExpectedSalaryAnnual,
                request.DateOfBirth.ToString()
            );
        }

        public async Task<JobSeekerUser> GetJobSeekerUser(string appUserEmail)
        {
            var user = await _dataContext.JobSeekerUsers.FromSqlRaw<JobSeekerUser>(
                "exec spGetJobSeekerFromAppUserEmail {0}",
                appUserEmail
            ).SingleOrDefaultAsync();
            return user;
        }

        public async Task UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            var result = await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spUpdateJobSeekerUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                request.FirstName,
                request.LastName,
                request.Email,
                request.AppUserEmail,
                request.Phone,
                request.Address,
                request.TotalExperience,
                request.ExpectedSalaryAnnual,
                request.DateOfBirth.ToString()
            );
        }

        public async Task DeleteJobSeekerUser(string appUserEmail)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spDeleteJobSeekerUser {0}",
                appUserEmail
            );
        }
    }
}