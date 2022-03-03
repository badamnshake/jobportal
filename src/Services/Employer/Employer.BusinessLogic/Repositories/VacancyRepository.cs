using System;
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

        public async Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromLastDate(DateTime lastDate,
            PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.LastDateToApply.CompareTo(lastDate) > 0)
                .OrderBy(v => v.LastDateToApply)
                .ProjectTo<VacancyReponseDetailsDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            return await PagedList<VacancyReponseDetailsDto>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromPublishedDate(DateTime publishedDate,
            PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.PublishedDate.CompareTo(publishedDate) > 0)
                .OrderBy(v => v.PublishedDate)
                .ProjectTo<VacancyReponseDetailsDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            return await PagedList<VacancyReponseDetailsDto>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }
        //

        public async Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromOrganization(string organizationName,
            PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.PublishedBy == organizationName.Trim())
                    .OrderBy(v => v.LastDateToApply)
                    .ProjectTo<VacancyReponseDetailsDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                ;
            return await PagedList<VacancyReponseDetailsDto>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromSalary(int minSalary,
            PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.MinSalary >= minSalary)
                    .OrderByDescending(v => v.MinSalary)
                    .ProjectTo<VacancyReponseDetailsDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                ;
            return await PagedList<VacancyReponseDetailsDto>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<PagedList<VacancyReponseDetailsDto>> GetVacanciesFromLocation(string location,
            PageParams pageParams)
        {
            var query = _dataContext.Vacancies.Where(x => x.Location.Contains(location))
                    .OrderBy(v => v.LastDateToApply)
                    .ProjectTo<VacancyReponseDetailsDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                ;
            return await PagedList<VacancyReponseDetailsDto>.CreateAsync(query, pageParams.PageNumber,
                pageParams.PageSize);
        }
    }
}