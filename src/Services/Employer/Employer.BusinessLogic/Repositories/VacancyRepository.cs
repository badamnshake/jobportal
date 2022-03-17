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

        public async Task<PagedList<ResponseVacancyDetails>> GetVacancies(VacancyParams vacancyParams)
        {
            // this method has various filtration
            // creates query from the coming variables
            var query = _dataContext.Vacancies
                .ProjectTo<ResponseVacancyDetails>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsQueryable();
            if (!vacancyParams.AnyFilters)
            {
                query = query.Where(v => v.Id > ((vacancyParams.PageNumber - 1) * vacancyParams.PageSize));
                return await PagedList<ResponseVacancyDetails>.CreateAsync(query, vacancyParams.PageNumber,
                    vacancyParams.PageSize, false);
            }

            if (vacancyParams.Location != null)
                query = query.Where(v => v.Location.Contains(vacancyParams.Location));
            if (vacancyParams.MinSalary != 0)
                // more than min salary records will be shows
                query = query.Where(v => v.MinSalary > vacancyParams.MinSalary);
            if (vacancyParams.MaxSalary != 0)
                // more than max salary records will be shows
                query = query.Where(v => v.MaxSalary < vacancyParams.MaxSalary);
            if (vacancyParams.LastDateToApply != default)
                // only vacancies when the last date is after the applied date
                query = query.Where(v => v.LastDateToApply.CompareTo(vacancyParams.LastDateToApply) >= 0);
            // only vacancies when the published date is after the applied date
            if (vacancyParams.PublishedDate != default)
                query = query.Where(x => x.PublishedDate.CompareTo(vacancyParams.PublishedDate) > 0);

            query = vacancyParams.OrderBy switch
            {
                ToOrderBy.MinSalaryAscending => query.OrderBy(v => v.MinSalary),
                ToOrderBy.MinSalaryDescending => query.OrderByDescending(v => v.MinSalary),
                ToOrderBy.MaxSalaryDescending => query.OrderByDescending(v => v.MaxSalary),
                ToOrderBy.LastDateToApply => query.OrderByDescending(v => v.LastDateToApply),
                ToOrderBy.PublishedDate => query.OrderBy(v => v.PublishedDate),
                _ => query.OrderBy(v => v.Id)
            };

            // maybe in future the app only shows vacancies where last date isn't expired
            // meaning last date is after today, only those entries will be shown
            // query = query.Where(v => v.LastDateToApply.CompareTo(DateTime.Today) > 0);

            return await PagedList<ResponseVacancyDetails>.CreateAsync(query, vacancyParams.PageNumber,
                vacancyParams.PageSize, true);
        }
    }
}