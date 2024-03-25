using Data.Context;
using Domain.OrganizationNS;
using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS.Queries;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly OrganizationDBContext _context;

        public OrganizationRepository(OrganizationDBContext context)
        {
            _context = context;
        }

        public async Task<Organization> Create(Organization organization)
        {
            await _context.AddAsync(organization);
            _context.SaveChanges();
            return organization;
        }

        public async Task<bool> Delete(Organization organization)
        {
            try
            {
                _context.Remove(organization);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<Organization>> Get(OrganizationQuery query)
        {
            return _context.Organization
                .Where(x => (query.UId != null && query.UId.Contains(x.UId)) || (query.Name != null && query.Name.Contains(x.Name)))
                .Include("Contact")
                .ToList();
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAll()
        {
            return _context.Organization.Select(x => new OrganizationDTO() { UId = x.UId, Name = x.Name, Description = x.Description}).ToList();
        }

        public async Task<Organization> Login(string username, string password)
        {
            return _context.Organization.Where(x => x.Login == username && x.Password == password).FirstOrDefault();
        }

        public async Task<Organization> Update(Organization organization)
        {
            var orgModel = new
            {
                Name = organization.Name,
                Description = organization.Description,
                Contact = new
                {
                    Id = organization.Contact.Id,
                    Phone = organization.Contact.Phone,
                    Email = organization.Contact.Email
                }
            };
            var organizationToUpdate = _context.Organization.FirstOrDefault(x => x.UId == organization.UId);

            _context.Organization.Entry(organizationToUpdate).CurrentValues.SetValues(orgModel);
            await _context.SaveChangesAsync();
            return organizationToUpdate;
        }
    }
}
