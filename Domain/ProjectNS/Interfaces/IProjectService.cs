using Domain.DeveloperNS;
using Domain.ProjectNS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS.Interfaces;
public interface IProjectService
{
    Task<IEnumerable<dynamic>> Get(ProjectQuery query);
    Task<Project> Create(Project organization);
    Task<Project> Update(Project organization);
    Task<bool> Delete(Guid project);
    Task<bool> AddProjectDeveloper(Guid developerId, Guid projectId, Guid organizationId);
    Task RemoveProjectDeveloper(Guid organizationId, Guid projectId, Guid developerId);

    Task<ICollection<Developer>> GetDevelopersByProject(Guid organizationId, Guid projectId);
}
