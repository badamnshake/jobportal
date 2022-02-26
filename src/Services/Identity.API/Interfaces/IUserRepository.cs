
using System.Threading.Tasks;
using Identity.API.DTOs;
using Identity.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Register(User user);
        Task<bool> ChangePassword(User user, string newPassword);
        Task<bool> DoesUserExist(string email);
        Task<User> GetUserAsync(string email);   

    }
}