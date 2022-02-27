using System.Threading.Tasks;
using Employer.API.Entities;
using Employer.API.Interfaces;
namespace Employer.API.Data
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly DataContext _dataContext;
        public EmployerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<EmployerEntity> AddEmployerDetailsAsync(EmployerEntity employerEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<EmployerEntity> GetEmployerAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<EmployerEntity> UpdateEmployerAsync(EmployerEntity employerEntity)
        {
            throw new System.NotImplementedException();
        }
    }

}