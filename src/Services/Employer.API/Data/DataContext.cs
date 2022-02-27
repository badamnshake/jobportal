using Microsoft.EntityFrameworkCore;
using Employer.API.Entities;

namespace Employer.API.Data
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