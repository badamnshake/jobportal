using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.BusinessLogic.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly DataContext _dataContext;

        public ExperienceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddExperience(ReqAddExp request)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spAddExperience {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                request.JobSeekerUserId,
                request.CompanyName,
                request.CompanyUrl,
                request.StartDate.Date,
                request.EndDate.Date,
                request.Designation,
                request.JobDescription
            );
        }

        public async Task DeleteExperience(ReqDelExp request)
        {
            await _dataContext.Database.ExecuteSqlRawAsync(
                "exec spDeleteExperience {0}",
                request.Id
            );
        }

        public async Task<IEnumerable<Experience>> GetExperiencesOfJobSeeker(RequestJobSeekerId request)
        {
            var jobSeeker = await _dataContext.JobSeekerUsers
                .Include(u => u.Experiences)
                .SingleOrDefaultAsync(u => u.Id == request.Id);
            return jobSeeker.Experiences;
        }
    }
}