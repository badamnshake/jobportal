using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class JobSeekerUserService: IJobSeekerUserService
    {
        private readonly HttpClient _httpClient;
        public JobSeekerUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        
    }
}