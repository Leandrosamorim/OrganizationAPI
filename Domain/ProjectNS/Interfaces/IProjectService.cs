using Domain.ProjectNS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS.Interfaces;
public interface IProjectService
{
    Task<IEnumerable<Project>> Get(ProjectQuery query);
    Task<Project> Create(Project organization);
    Task<Project> Update(Project organization);
    Task Delete(Guid project);
    Task<bool> AddProjectDeveloper(Guid developerId, Guid projectId, Guid organizationId);
    Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper);
}
