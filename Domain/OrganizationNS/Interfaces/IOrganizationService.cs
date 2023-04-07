using Domain.OrganizationNS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationNS.Interfaces
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDTO>> GetAll();
        Task<IEnumerable<Organization>> Get(OrganizationQuery query);
        Task<Organization> Create (Organization organization);
        Task<bool> Delete (Guid organizationUId);
        Task<Organization> Update (Organization organization);
        Task<Organization> Login (string login, string password);
        
    }
}
