using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employer.API.Entities;
using Employer.API.Helpers;
using Employer.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employer.API.Data
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DataContext _dataContext;
        public VacancyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Vacancy> AddVacancy(Vacancy vacancy)
        {
            var createdVacancy = await _dataContext.Vacancies.AddAsync(vacancy);
            await _dataContext.SaveChangesAsync();
            return createdVacancy.Entity;
        }
        public async Task<bool> UpdateVacancy(Vacancy vacancy)
        {
            _dataContext.Vacancies.Update(vacancy);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteVacancy(Vacancy vacancy)
        {
            _dataContext.Vacancies.Remove(vacancy);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        // public async Task<bool> DoesVacancyExist(int id)
        // {
        //     return await _dataContext.Vacancies.FindAsync(id) != null;
        // }
        // return null if vacancy is not found
        public async Task<Vacancy> GetVacancyDetails(int id)
        {
            return await _dataContext.Vacancies.FindAsync(id);
        }
        public async Task<PagedList<Vacancy>> GetVacanciesFromLastDate(DateTime LastDate, PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.LastDateToApply.CompareTo(LastDate) > 0).AsNoTracking();
            return await PagedList<Vacancy>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);

        }
        public async Task<IEnumerable<Vacancy>> GetVacanciesFromPublishedDate(DateTime publishedDate)
        {
            var vacancies = await _dataContext.Vacancies.Where(x => x.PublishedDate.CompareTo(publishedDate) > 0).ToListAsync();
            return vacancies;
        }
        public async Task<IEnumerable<Vacancy>> GetVacanciesFromOrganization(string organizationName)
        {
            var vacancies = await _dataContext.Vacancies.Where(x => x.PublishedBy == organizationName.Trim()).ToListAsync();
            return vacancies;
        }

        // public async Task<IEnumerable<Vacancy>> GetVacanciesFromOrganizationType(string organizationType)
        // {
        //     var vacancies = await _dataContext.Vacancies.Where(x => x.PublishedBy == organizationName.Trim()).ToListAsync();
        //     throw new NotImplementedException();
        // }


        public async Task<IEnumerable<Vacancy>> GetVacanciesFromSalary(int minSalary)
        {
            var vacancies = await _dataContext.Vacancies.Where(x => x.MinSalary >= minSalary).ToListAsync();
            return vacancies;
        }
        public async Task<IEnumerable<Vacancy>> GetVacanciesFromLocation(string location)
        {
            var vacancies = await _dataContext.Vacancies.Where(x => x.Location.Contains(location)).ToListAsync();
            return vacancies;
        }
    }

}