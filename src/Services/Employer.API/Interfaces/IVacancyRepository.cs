
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employer.API.Entities;
using Employer.API.Helpers;

namespace Employer.API.Interfaces
{
    public interface IVacancyRepository
    {
        Task<int> AddVacancy(Vacancy vacancy);
        Task<bool> UpdateVacancy(Vacancy vacancy);
        Task<bool> DeleteVacancy(Vacancy vacancy);
        Task<Vacancy> GetVacancyDetails(int id);
        // Task<bool> DoesVacancyExist(int id);

        // get vacancies from some...
        Task<IEnumerable<Vacancy>> GetVacanciesFromOrganization(string organizationName);
        // Task<IEnumerable<Vacancy>> GetVacanciesFromOrganizationType(string organizationType);
        Task<IEnumerable<Vacancy>> GetVacanciesFromPublishedDate(DateTime publishedDate);
        Task<PagedList<Vacancy>> GetVacanciesFromLastDate(DateTime lastDate, PageParams pageParams);
        Task<IEnumerable<Vacancy>> GetVacanciesFromSalary(int minSalary);
        Task<IEnumerable<Vacancy>> GetVacanciesFromLocation(string location);
    }
}