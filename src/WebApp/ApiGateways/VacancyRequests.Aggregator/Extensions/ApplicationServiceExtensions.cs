using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt =>
            {
                var key = Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:key"));
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = config.GetValue<string>("Jwt:Issuer"),
                   
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });
            return services;
        }
    }

}