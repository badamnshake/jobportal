using AutoMapper;
using JobSeeker.API.Models;
using JobSeeker.API.DTOs;

namespace JobSeeker.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobSeekerUser,ResponseJobSeekerUser >();
        }
    }
}