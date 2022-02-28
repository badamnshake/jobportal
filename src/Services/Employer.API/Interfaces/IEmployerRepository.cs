using System.Collections.Generic;
using System.Threading.Tasks;

using Employer.API.Entities;
namespace Employer.API.Interfaces
{
    public interface IEmployerRepository
    {
        Task<EmployerEntity> GetEmployer(string userEmail);
        Task<bool> AddEmployerDetails(EmployerEntity employerEntity);
        Task<bool> UpdateEmployerDetail(EmployerEntity employerEntity);
        Task<bool> DoesEmployerExist(string userEmail);

    }
}