using System.Threading.Tasks;
using AutoMapper;
using JobSeeker.API.DTOs;
using JobSeeker.API.Interfaces;
using JobSeeker.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.API.Data
{
    public class JobSeekerUserRepository : IJobSeekerUserRepository
    {
        private readonly DataContext _dataContext;
        public JobSeekerUserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddJobSeekerUser(RequestJobSeekerUser request)
        {
            try
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
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<JobSeekerUser> GetJobSeekerUser(RequestAppUserEmail request)
        {
            var user = await _dataContext.JobSeekerUsers.FromSqlRaw<JobSeekerUser>(
               "exec spGetJobSeekerFromAppUserEmail {0}",
               request.AppUserEmail
            ).ToListAsync();
            return user[0];
        }

        public async Task<bool> UpdateJobSeekerUser(RequestJobSeekerUser request)
        {
            try
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
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteJobSeekerUser(RequestAppUserEmail request)
        {
            try
            {
                var result = await _dataContext.Database.ExecuteSqlRawAsync(
                   "exec spDeleteJobSeekerFromAppUserEmail {0}",
                   request.AppUserEmail
                );
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}