using System.Net.Http;
using System.Threading.Tasks;
using ApiGateway.Infrastructure.Identity.RequestResponseModels;

namespace ApiGateway.Services.Interfaces
{
    public interface IUserService
    {

        public Task<HttpResponseMessage> ChangePassword(ChangePasswordDto changePasswordDto);
        public Task<HttpResponseMessage> Register(RegisterDto registerDto);
        public Task<HttpResponseMessage> Login(LoginDto loginDto);
    }

}