using Domain.OrganizationNS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationNS.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationDTO>> GetAll();

        Task<IEnumerable<Organization>> Get(OrganizationQuery query);
        Task<Organization> Create(Organization organization);
        Task<Organization> Update(Organization organization);
        Task<bool> Delete(Organization organization);
        Task<Organization> Login(string username, string password);
    }
}
