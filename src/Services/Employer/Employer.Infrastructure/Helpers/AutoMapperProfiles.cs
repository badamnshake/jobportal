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
            
            // this map ensures when updating the entity if null value is passed it will not update it
            CreateMap<RequestEmployerDetails, EmployerEntity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RequestVacancyDetails, Vacancy>();

            // this map ensures when updating the entity if null value is passed it will not update it
            CreateMap<RequestVacancyUpdate, Vacancy>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Vacancy, ResponseVacancyDetails>();
        }
    }
}