using System.Threading.Tasks;
using Employer.API.Entities;
using Employer.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employer.API.Data
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly DataContext _dataContext;
        public EmployerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
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
        public async Task<EmployerEntity> GetEmployer(string userEmail)
        {
            return await _dataContext.EmployerEntities.SingleOrDefaultAsync(x => x.CreatedByEmailUser == userEmail);
        }
        public async Task<bool> UpdateEmployerDetail(EmployerEntity employerEntity)
        {
            _dataContext.EmployerEntities.Update(employerEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }

}