using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employer.API.Entities;
using Employer.API.Interfaces;
namespace Employer.API.Data
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DataContext _dataContext;
        public VacancyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<int> AddVacancy()
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromLastDate(DateTime LastDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromOrganization(string organizationName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromOrganizationType(string organizationType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromPublishedDate(DateTime publishedDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromSalary(int min)
        {
            throw new NotImplementedException();
        }

        public Task<Vacancy> GetVacancyDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Vacancy> GetVacancyDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacancy>> GetVacanciesFromLocation(int min)
        {
            throw new NotImplementedException();
        }
    }

}