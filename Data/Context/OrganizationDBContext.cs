using Domain.ContactNS;
using Domain.OrganizationNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    }
}
