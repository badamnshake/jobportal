using Identity.Infrastructure.Models;

namespace Identity.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}