using System.Threading.Tasks;
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

        /// <summary>
        /// Gets employer from id returns null if not found
        /// </summary>
        /// <param name="id">id of the employer</param>
        /// <returns>employer entity or null </returns>
        public async Task<EmployerEntity> GetEmployerFromId(int id)
        {
            // get employer and include vacancy
            return await _dataContext.EmployerEntities
                .Include(x => x.Vacancies)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// creates employer details
        /// </summary>
        /// <param name="employerEntity"> the employer entity to save</param>
        /// <returns> true or false based on success</returns>
        public async Task<bool> CreateEmployerDetails(EmployerEntity employerEntity)
        {
            await _dataContext.EmployerEntities.AddAsync(employerEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// checks if an employer profile exists
        /// </summary>
        /// <param name="userEmail">user email which corresponds to token and identity db</param>
        /// <returns>true or false on profiles existence</returns>
        public async Task<bool> DoesEmployerExist(string userEmail)
        {
            return await _dataContext.EmployerEntities.AnyAsync(x => x.CreatedByEmailUser == userEmail);
        }

        public async Task<bool> DoesEmployerExistById(int id)
        {
            return await _dataContext.EmployerEntities.FindAsync(id) != null;
        }

        /// <summary>
        /// deletes and employer from the database
        /// </summary>
        public async Task<bool> DeleteEmployer(string email)
        {
            var emp = await _dataContext.EmployerEntities
                .SingleOrDefaultAsync(x => x.CreatedByEmailUser == email);
            _dataContext.EmployerEntities.Remove(emp);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<EmployerEntity> GetEmployer(string userEmail)
        {
            return await _dataContext.EmployerEntities
                .Include(x => x.Vacancies)
                .SingleOrDefaultAsync(x => x.CreatedByEmailUser == userEmail);
        }

        /// <summary>
        /// updates employer details
        /// </summary>
        /// <param name="employerEntity"> the employer entity to save</param>
        /// <returns> true or false based on success</returns>
        public async Task<bool> UpdateEmployerDetail(EmployerEntity employerEntity)
        {
            _dataContext.EmployerEntities.Update(employerEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}