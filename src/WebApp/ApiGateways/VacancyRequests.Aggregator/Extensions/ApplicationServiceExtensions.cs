using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacancyRequests.Aggregator.Services;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IEmployerService, EmployerService>(c =>
            {
                c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:EmployerUrl"));
            });
            // services.AddHttpClient<IJobSeekerUserService, JobSeekerUserService>(
            //     c => { c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            // );
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