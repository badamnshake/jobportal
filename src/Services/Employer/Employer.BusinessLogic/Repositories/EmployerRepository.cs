using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Employer.BusinessLogic.Interfaces;
using Employer.DataAccess;
using Employer.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Employer.BusinessLogic.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly DataContext _dataContext;

        public EmployerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<EmployerEntity> GetEmployerFromId(int id)
        {
            return await _dataContext.EmployerEntities
                .Include(x => x.Vacancies)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateEmployerDetails(EmployerEntity employerEntity)
        {
            await _dataContext.EmployerEntities.AddAsync(employerEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DoesEmployerExist(string userEmail)
        {
            return await _dataContext.EmployerEntities.AnyAsync(x => x.CreatedByEmailUser == userEmail);
        }
        public async Task<bool> DoesEmployerExistById(int id)
        {
            return await _dataContext.EmployerEntities.FindAsync(id) != null;
        }

        public async Task<EmployerEntity> GetEmployer(string userEmail)
        {
            return  await _dataContext.EmployerEntities
                .Include(x => x.Vacancies)
                .SingleOrDefaultAsync(x => x.CreatedByEmailUser == userEmail);
        }

        public async Task<bool> UpdateEmployerDetail(EmployerEntity employerEntity)
        {
            _dataContext.EmployerEntities.Update(employerEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}