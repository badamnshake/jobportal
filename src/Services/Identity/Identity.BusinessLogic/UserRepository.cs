using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.BusinessLogic.Interfaces;
using Identity.DataAccess;
using Identity.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.BusinessLogic
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // registers a user save changes async confirms the registration
        public async Task<bool> Register(User user)
        {
            _dataContext.Add(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }


        // this method will be needed to authenticate
        public async Task<bool> ChangePassword(User user, string newPassword)
        {
            // save password salt and new hashed password
            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            user.PasswordSalt = hmac.Key;

            _dataContext.Users.Update(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            // checks if an user exists in the database
            return await _dataContext.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task DeleteUser(string email)
        {
            // if the user doesn't exist there's no need to delete the user
            // this check is redundant but I still have added it just in case
            var user = await GetUserAsync(email);
            if (user == null) return;
            // remove the user
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }
    }
}