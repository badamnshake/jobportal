using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class VacancyRequestService: IVacancyRequestService
    {
        private readonly HttpClient _httpClient;
        public VacancyRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        
    }
}