using System.Threading.Tasks;
using Identity.Infrastructure.Models;

namespace Identity.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Register(User user);
        Task<bool> ChangePassword(User user, string newPassword);
        Task<bool> DoesUserExist(string email);
        Task<User> GetUserAsync(string email);
        Task DeleteUser(string email);
    }
}