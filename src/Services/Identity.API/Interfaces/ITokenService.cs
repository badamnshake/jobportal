using Identity.API.Entities;

namespace Identity.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}