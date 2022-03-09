using System.Threading.Tasks;
using Employer.Infrastructure.Helpers;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels.Vacancy;

namespace Employer.BusinessLogic.Interfaces
{
    public interface IVacancyRepository
    {
        Task<Vacancy> AddVacancy(Vacancy vacancy);
        Task<bool> UpdateVacancy(Vacancy vacancy);
        Task<bool> DeleteVacancy(Vacancy vacancy);

        Task<Vacancy> GetVacancyDetails(int id);

        // get vacancies from some...
        Task<PagedList<VacancyResponseDetailsDto>> GetVacancies(VacancyParams vacancyParams);
    }
}