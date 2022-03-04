using System.Text;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.BusinessLogic.Repositories;
using JobSeeker.DataAccess;
using JobSeeker.Infrastrucure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JobSeeker.API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
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
            // services.AddScoped<IJobSeekerUserRepository, JobSeekerUserRepository>();
            services.AddScoped<IJobSeekerUserRepository, JobSeekerUserRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}