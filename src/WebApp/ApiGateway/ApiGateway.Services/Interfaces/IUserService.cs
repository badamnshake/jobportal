using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Infrastructure.Identity.RequestResponseModels;

namespace ApiGateway.Services.Interfaces
{
    public interface IUserService
    {

        public Task<ActionResult<UserDto>> ChangePassword(ChangePasswordDto changePasswordDto);
    }

}