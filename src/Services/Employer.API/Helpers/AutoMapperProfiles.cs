using AutoMapper;
using Employer.API.Entities;
using Employer.API.DTOs;

namespace Employer.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployerEntity, EmployerResponseDto>();
            CreateMap<DetailsDto, EmployerEntity>();
            CreateMap<VacancyDetailsDto, Vacancy>();
            CreateMap<Vacancy, VacancyReponseDetailsDto>();
        }
    }
}