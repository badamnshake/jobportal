using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<HttpResponseMessage> CreateVacancyRequest(int vacancyId)
        {
            var values = new Dictionary<string, string>();
            values.Add("vacancyId", vacancyId.ToString());
            var content = new FormUrlEncodedContent(values);
            var response = await _client.PostAsync("api/vacancy-request/create", content);
            return response;
        }

        public async Task<HttpResponseMessage> GetAppliedJobSeekersOnAVacancy(int vacancyId)
        {
            var response = await _client.GetAsync($"api/vacancy-request/get/{vacancyId}");
            return response;
        }
    }
}