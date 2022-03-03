using System;
using System.Collections.Generic;
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
        Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromOrganization(string organizationName, PageParams pageParams);

        // Task<IEnumerable<Vacancy>> GetVacanciesFromOrganizationType(string organizationType);
        Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromPublishedDate(DateTime publishedDate, PageParams pageParams);
        Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromLastDate(DateTime lastDate, PageParams pageParams);
        Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromSalary(int minSalary, PageParams pageParams);
        Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromLocation(string location, PageParams pageParams);
    }
}