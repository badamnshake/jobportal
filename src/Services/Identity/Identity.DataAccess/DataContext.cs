using Identity.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // field vs property
        public DbSet<User> Users { get; set; }
    }
}