using System;
using ApiGateway.Services.Interfaces;
using ApiGateway.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiGateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // http clients
            services.AddHttpClient<IEmployerService, EmployerService>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:EmployerUrl"));
            });
            services.AddHttpClient<IExperienceService, ExperienceService>(
                c => { c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IJobSeekerUserService, JobSeekerUserService>(
                c => { c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IQualificationService, QualificationService>(
                c => { c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IUserService, UserService>(c =>
                {
                    c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:IdentityUrl"));
                }
            );
            services.AddHttpClient<IVacancyRequestService, VacancyRequestService>(
                c => { c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:JobSeekerUrl")); }
            );
            services.AddHttpClient<IVacancyService, VacancyService>(c =>
                {
                    c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:EmployerUrl"));
                }
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ApiGateway.API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}