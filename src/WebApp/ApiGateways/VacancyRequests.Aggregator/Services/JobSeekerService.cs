using System.Net.Http;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly HttpClient _client;

        public JobSeekerService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> DeleteJobSeeker(string email)
        {
            return await _client.DeleteAsync($"api/job-seeker/delete/{email}");
        }
    }
}