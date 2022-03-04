using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiGateway.Infrastructure.Identity.RequestResponseModels;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class UserService: IUserService
    {
        private readonly HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            var response = await _client.PostAsJsonAsync("api/user/register", registerDto);
            return response;
        }

        public async Task<HttpResponseMessage> Login(LoginDto loginDto)
        {
            var response = await _client.PostAsJsonAsync("api/user/login", loginDto);
            return response;
        }
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var response = await _client.PutAsJsonAsync("api/user/change-password", changePasswordDto);
            return response;
        }
    }
}