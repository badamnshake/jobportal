using System.Net.Http;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly HttpClient _client;

        public EmployerService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetDetails(string email)
        {
            var response = await _client.GetAsync($"api/employer/get/{email}");
            
            return response;
        }
    }
}