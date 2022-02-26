using Microsoft.EntityFrameworkCore;

namespace Employer.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}