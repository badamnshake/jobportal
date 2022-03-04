using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly HttpClient _httpClient;

        public EmployerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}