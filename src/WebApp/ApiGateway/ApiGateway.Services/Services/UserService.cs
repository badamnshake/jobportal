using System.Net.Http;
using System.Threading.Tasks;
using ApiGateway.Infrastructure.Identity.RequestResponseModels;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class UserService: IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ActionResult<UserDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            throw new System.NotImplementedException();
        }
    }
}