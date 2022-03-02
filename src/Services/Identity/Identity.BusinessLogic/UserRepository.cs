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

        public DbSet<User> Users;

        public async Task<bool> Register(User user)
        {
            try
            {
                _dataContext.Add(user);
                await _dataContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }

            return true;
        }


        // this method will be needed to authenticate
        public async Task<bool> ChangePassword(User user, string newPassword)
        {
            try
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                user.PasswordSalt = hmac.Key;

                _dataContext.Users.Update(user);
                await _dataContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }

            return true;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            return await _dataContext.Users.AnyAsync<User>(x => x.Email == email);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}