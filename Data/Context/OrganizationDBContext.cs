using Domain.ContactNS;
using Domain.OrganizationNS;
using Domain.ProjectNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Data.Context
{
    public class OrganizationDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public OrganizationDBContext(DbContextOptions<OrganizationDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectFeedback> ProjectFeedback { get; set; }
        public virtual DbSet<ProjectDeveloper> ProjectDeveloper { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectDeveloper>()
                .HasKey(pd => new { pd.ProjectId, pd.DeveloperId });

            modelBuilder.Entity<ProjectDeveloper>()
                .HasOne(pd => pd.Project)
                .WithMany(p => p.Developers)
                .HasForeignKey(pd => pd.ProjectId);

            modelBuilder.Entity<ProjectDeveloper>()
                .HasOne(pd => pd.Developer)
                .WithMany(d => d.ProjectDevelopers)
                .HasForeignKey(pd => pd.DeveloperId);
        }
    }
}
