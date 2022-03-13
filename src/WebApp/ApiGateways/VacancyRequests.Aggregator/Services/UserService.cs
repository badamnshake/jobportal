using System.Net.Http;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Services.Interfaces;

namespace VacancyRequests.Aggregator.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> DeleteUser(string email)
        {
            return await _client.DeleteAsync($"api/user/delete/{email}");
        }
    }
}