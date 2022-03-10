using AutoMapper;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;

namespace JobSeeker.Infrastrucure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobSeekerUser, ResponseJobSeekerUser>();
            CreateMap<Qualification, ResQualification>();
            CreateMap<Experience, ResExperience>();
            CreateMap<VacancyRequest, ResVacancyId>();
        }
    }
}