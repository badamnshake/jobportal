using JobSeeker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<JobSeekerUser> JobSeekerUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobSeekerUser>().
            HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<JobSeekerUser>().
            HasIndex(u => u.AppUserEmail)
            .IsUnique();
        }
    }
}