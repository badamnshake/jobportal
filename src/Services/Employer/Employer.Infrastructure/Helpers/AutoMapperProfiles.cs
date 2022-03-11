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
            // required mappings from request to object
            // and object in response
            CreateMap<EmployerEntity, ResponseEmployerDetails>();
            CreateMap<RequestEmployerDetails, EmployerEntity>();
            CreateMap<RequestVacancyDetails, Vacancy>();
            CreateMap<Vacancy, ResponseVacancyDetails>();
        }
    }
}