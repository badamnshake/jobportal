using Employer.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Employer.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmployerEntity> EmployerEntities { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}