using JobSeeker.Infrastrucure.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<JobSeekerUser> JobSeekerUsers { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<VacancyRequest> VacancyRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobSeekerUser>().HasIndex(u => u.AppUserEmail)
                .IsUnique();
        }
    }
}