using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class VacancyService: IVacancyService
    {
        private readonly HttpClient _httpClient;
        public VacancyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        
    }
}