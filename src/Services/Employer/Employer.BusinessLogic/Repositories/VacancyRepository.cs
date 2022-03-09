using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employer.BusinessLogic.Interfaces;
using Employer.DataAccess;
using Employer.Infrastructure.Helpers;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels.Vacancy;
using Microsoft.EntityFrameworkCore;

namespace Employer.BusinessLogic.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public VacancyRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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

        public async Task<Vacancy> GetVacancyDetails(int id)
        {
            return await _dataContext.Vacancies.FindAsync(id);
        }

        public async Task<PagedList<VacancyResponseDetailsDto>> GetVacancies(VacancyParams vacancyParams)
        {
            var query = _dataContext.Vacancies
                .ProjectTo<VacancyResponseDetailsDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsQueryable();
            if (vacancyParams.Location != null)
                query = query.Where(v => v.Location.Contains(vacancyParams.Location));
            if (vacancyParams.MinSalary != 0)
                query = query.Where(v => v.MinSalary > vacancyParams.MinSalary);
            if (vacancyParams.MaxSalary != 0)
                query = query.Where(v => v.MaxSalary > vacancyParams.MaxSalary);
            if (vacancyParams.LastDateToApply != default)
                query = query.Where(v => v.LastDateToApply.CompareTo(vacancyParams.LastDateToApply) > 0);
            if (vacancyParams.PublishedDate != default)
                query = query.Where(x => x.PublishedDate.CompareTo(vacancyParams.PublishedDate) > 0);

            switch (vacancyParams.OrderBy)
            {
                case ToOrderBy.MinSalaryDescending:
                    query = query.OrderByDescending(v => v.MinSalary);
                    break;
                case ToOrderBy.MaxSalaryDescending:
                    query = query.OrderByDescending(v => v.MaxSalary);
                    break;
                case ToOrderBy.LastDateToApply:
                    query = query.OrderByDescending(v => v.LastDateToApply);
                    break;
                case ToOrderBy.PublishedDate:
                    query = query.OrderBy(v => v.PublishedDate);
                    break;
                default:
                    query = query.OrderBy(v => v.MinSalary);
                    break;
                    
            }

            // query = query.Where(v => v.LastDateToApply.CompareTo(DateTime.Today) > 0);

            return await PagedList<VacancyResponseDetailsDto>.CreateAsync(query, vacancyParams.PageNumber,
                vacancyParams.PageSize);
        }
    }
}