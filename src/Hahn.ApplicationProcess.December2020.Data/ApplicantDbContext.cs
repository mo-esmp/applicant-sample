using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicantEntity> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicantDbContext).Assembly);
        }
    }
}