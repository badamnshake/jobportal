
using Identity.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data 
{
    public class DataContext: DbContext {

        public DataContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<User> Users;

    }
}