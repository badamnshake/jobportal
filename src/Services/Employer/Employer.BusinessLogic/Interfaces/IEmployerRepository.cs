using System.Threading.Tasks;
using Employer.Infrastructure.Models;

namespace Employer.BusinessLogic.Interfaces
{
    public interface IEmployerRepository
    {
        Task<EmployerEntity> GetEmployer(string userEmail);
        Task<EmployerEntity> GetEmployerFromId(int id);
        Task<bool> CreateEmployerDetails(EmployerEntity employerEntity);
        Task<bool> UpdateEmployerDetail(EmployerEntity employerEntity);
        Task<bool> DoesEmployerExist(string userEmail);
        Task<bool> DoesEmployerExistById(int id);
        Task<bool> DeleteEmployer(string email);
    }
}