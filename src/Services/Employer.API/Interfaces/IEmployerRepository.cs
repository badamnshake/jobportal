using System.Collections.Generic;
using System.Threading.Tasks;

using Employer.API.Entities;
namespace Employer.API.Interfaces
{
    public interface IEmployerRepository
    {
        Task<EmployerEntity> GetEmployerAsync(string email);
        Task<EmployerEntity> AddEmployerDetailsAsync(EmployerEntity employerEntity);
        Task<EmployerEntity> UpdateEmployerAsync(EmployerEntity employerEntity);
        Task<bool> SaveAllChangesAsync();

    }
}