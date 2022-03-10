using AutoMapper;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels.Employer;
using Employer.Infrastructure.RequestResponseModels.Vacancy;

namespace Employer.Infrastructure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployerEntity, ResponseEmployerDetails>();
            CreateMap<RequestEmployerDetails, EmployerEntity>();
            CreateMap<RequestVacancyDetails, Vacancy>();
            CreateMap<Vacancy, ResponseVacancyDetails>();
        }
    }
}