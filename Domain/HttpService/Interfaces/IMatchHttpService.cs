using Domain.DeveloperNS;
using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HttpService.Interfaces
{
    public interface IMatchHttpService
    {
        public Task<IEnumerable<Developer>> GetMyMatches(Guid organizationUId);
        public Task<IEnumerable<DeveloperDTO>> GetDevelopersToMatch(Guid organizationUId, int stackId);
        public Task<bool> MatchDeveloper(Guid developerUId, Guid organizationUId);
    }
}
