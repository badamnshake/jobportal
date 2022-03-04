using System;
using ApiGateway.Services.Interfaces;
using ApiGateway.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IEmployerService, EmployerService>(c =>
            {
                c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:EmployerUrl"));
            });
            services.AddHttpClient<IExperienceService, ExperienceService>(
                c => { c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IJobSeekerUserService, JobSeekerUserService>(
                c => { c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IQualificationService, QualificationService>(
                c => { c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IUserService, UserService>(c =>
                {
                    c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:IdentityUrl"));
                }
            );
            services.AddHttpClient<IVacancyRequestService, VacancyRequestService>(
                c => { c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IVacancyService, VacancyService>(c =>
                {
                    c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:EmployerUrl"));
                }
            );
            return services;
        }
    }
}