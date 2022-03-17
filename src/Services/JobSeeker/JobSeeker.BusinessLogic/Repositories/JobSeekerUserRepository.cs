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

        /// <summary>
        /// create new instance/profile of a job seeker 
        /// </summary>
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
                request.DateOfBirth.ToString(CultureInfo.InvariantCulture)
            );
        }

        /// <summary>
        /// gets the job seeker from email
        /// </summary>
        /// <param name="appUserEmail">email which is in the token | foreign key in identity</param>
        public async Task<JobSeekerUser> GetJobSeeker(string appUserEmail)
        {
            var jobSeeker = await _dataContext.JobSeekerUsers.SingleOrDefaultAsync(j => j.AppUserEmail == appUserEmail);
            // var jobSeeker = await GetJobSeekerQuery(appUserEmail)
            // .SingleOrDefaultAsync();
            return jobSeeker;
        }

        /// <summary>
        /// Deletes job seeker from database
        /// </summary>
        /// <param name="jobSeekerUser"> the entity to delete</param>
        public async Task DeleteJobSeeker(JobSeekerUser jobSeekerUser)
        {
            _dataContext.JobSeekerUsers.Remove(jobSeekerUser);
            await _dataContext.SaveChangesAsync();
        }


        /// <summary>
        /// for an authorized job seeker return their details
        /// </summary>
        /// <param name="id">id of the job seeker</param>
        /// <returns>details of job seeker</returns>
        public async Task<JobSeekerUser> GetJobSeekerDetailsForJobSeeker(int id)
        {
            var jobSeeker = await _dataContext.JobSeekerUsers
                .Include(j => j.Qualifications)
                .Include(j => j.Experiences)
                .Include(j => j.VacancyRequests)
                .AsSplitQuery()
                .SingleOrDefaultAsync(j => j.Id == id);
            return jobSeeker;
        }

        /// <summary>
        /// updates the job seeker user
        /// </summary>
        /// <param name="request"> info to update</param>
        /// <param name="jobSeekerId">id of the js obj which got updated</param>
        public async Task UpdateJobSeeker(RequestJobSeekerUser request, int jobSeekerId)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spUpdateJobSeekerUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                jobSeekerId,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.Address,
                request.TotalExperience,
                request.ExpectedSalaryAnnual,
                request.DateOfBirth.ToString(CultureInfo.InvariantCulture)
            );
        }
    }
}