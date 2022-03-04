using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class ExperienceService: IExperienceService
    {
        private readonly HttpClient _httpClient;
        public ExperienceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        
    }
}