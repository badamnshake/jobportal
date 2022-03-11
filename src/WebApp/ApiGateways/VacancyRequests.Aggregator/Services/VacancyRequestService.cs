using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Models.Requests;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Services
{
    public class VacancyRequestService : IVacancyRequestService
    {
        private readonly HttpClient _client;

        public VacancyRequestService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> CreateVacancyRequest(int vacancyId, VacancyRequestModel vacancyRequest)
        {
            var response = await _client.PostAsJsonAsync($"api/vacancy-request/create/", vacancyRequest);
            return response;
        }

        public async Task<HttpResponseMessage> GetAppliedJobSeekersOnAVacancy(int vacancyId)
        {
            var response = await _client.GetAsync($"api/vacancy-request/get/{vacancyId}");
            return response;
        }
    }
}