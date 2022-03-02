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

        public async Task AddJobSeekerUser(RequestJobSeekerUser request)
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

        public async Task<JobSeekerUser> GetJobSeekerUser(RequestAppUserEmail request)
        {
            var user = await _dataContext.JobSeekerUsers.FromSqlRaw<JobSeekerUser>(
                "exec spGetJobSeekerFromAppUserEmail {0}",
                request.AppUserEmail
            ).ToListAsync();
            if (user.Count == 0) return null;
            return user[0];
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

        public async Task DeleteJobSeekerUser(RequestAppUserEmail request)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spDeleteJobSeekerUser {0}",
                request.AppUserEmail
            );
        }
    }
}