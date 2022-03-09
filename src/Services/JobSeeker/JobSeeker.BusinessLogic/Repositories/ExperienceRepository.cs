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

        public async Task AddExperience(ReqAddExp request, int jsId)
        {
            await  _dataContext.Database.ExecuteSqlRawAsync(
           // var query =  _dataContext.Experiences.FromSqlRaw(
                "exec spAddExperience {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                jsId,
                request.CompanyName,
                request.CompanyUrl,
                request.StartDate.Date,
                request.EndDate.Date,
                request.Designation,
                request.JobDescription
                );
           // await query.ToListAsync();

           // await _dataContext.Database.ExecuteSqlRawAsync();
        }

        public async Task DeleteExperience( int eId)
        {
            await  _dataContext.Database.ExecuteSqlRawAsync(
            // var query = _dataContext.Experiences.FromSqlRaw(
                "exec spDeleteExperience {0}",
                eId
                );
           // await query.ToListAsync();
            // await _dataContext.Database.ExecuteSqlRawAsync(
        }

    }
}