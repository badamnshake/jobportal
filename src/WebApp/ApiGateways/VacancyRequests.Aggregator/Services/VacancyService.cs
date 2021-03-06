using System.Net.Http;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly HttpClient _client;

        public VacancyService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetVacancy(int vacancyId)
        {
            var response = await _client.GetAsync($"api/vacancy/get/{vacancyId}");
            return response;
        }
        public async Task<HttpResponseMessage> DeleteVacancy(int vacancyId)
        {
            var response = await _client.GetAsync($"api/vacancy/delete/{vacancyId}");
            return response;
        }

    }
}