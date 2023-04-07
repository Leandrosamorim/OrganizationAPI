using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationNS.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Organization> Create(Organization organization)
            => await _organizationRepository.Create(organization);

        public async Task<bool> Delete(Guid organizationUId)
        {
            try
            {
                var query = new OrganizationQuery() { UId = new List<Guid>() { organizationUId } };
                var organization = _organizationRepository.Get(query).Result.FirstOrDefault();
                await _organizationRepository.Delete(organization);
                return true;
            }
            catch
            {
                return false;
            }
          
        }

        public async Task<IEnumerable<Organization>> Get(OrganizationQuery query)
            => await _organizationRepository.Get(query);

        public Task<IEnumerable<OrganizationDTO>> GetAll()
            => _organizationRepository.GetAll();

        public Task<Organization> Login(string login, string password)
            => _organizationRepository.Login(login, password);

        public async Task<Organization> Update(Organization organization)
            => await _organizationRepository.Update(organization);

    }
}
