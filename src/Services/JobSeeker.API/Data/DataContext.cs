using Microsoft.EntityFrameworkCore;

namespace JobSeeker.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}