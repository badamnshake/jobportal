using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<HttpResponseMessage> CreateVacancyRequest(VacancyRequestModel vacancyRequest)
        {
            var response = await _client.PostAsJsonAsync($"api/vacancy-request/create/", vacancyRequest);
            return response;
        }

        public async Task<HttpResponseMessage> GetAppliedJobSeekersOnAVacancy(int vacancyId, PageParams pageParams)
        {
            var query = HttpUtility.ParseQueryString(string.Empty
            );
            query["pageNumber"] = pageParams.PageNumber.ToString();
            query["pageSize"] = pageParams.PageSize.ToString();
            query["vacancyId"] = vacancyId.ToString();
            var queryString = query.ToString()!;
            var response = await _client.GetAsync("api/vacancy-request/get-job-seekers-on-a-vacancy/?" + queryString);
            return response;
        }
    }
}