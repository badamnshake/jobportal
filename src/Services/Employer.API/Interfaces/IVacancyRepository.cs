
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employer.API.Entities;

namespace Employer.API.Interfaces
{
    public interface IVacancyRepository
    {
        Task<int> AddVacancy();
        Task<bool> UpdateVacancy(Vacancy vacancy);
        Task<bool> DeleteVacancy(Vacancy vacancy);
        Task<Vacancy> GetVacancyDetails(int id);

        // get vacancies from some...
        Task<IEnumerable<Vacancy>> GetVacanciesFromOrganization(string organizationName);
        Task<IEnumerable<Vacancy>> GetVacanciesFromOrganizationType(string organizationType);
        Task<IEnumerable<Vacancy>> GetVacanciesFromPublishedDate(DateTime publishedDate);
        Task<IEnumerable<Vacancy>> GetVacanciesFromLastDate(DateTime LastDate);
        Task<IEnumerable<Vacancy>> GetVacanciesFromSalary(int min);
        Task<IEnumerable<Vacancy>> GetVacanciesFromLocation(int min);
    }
}