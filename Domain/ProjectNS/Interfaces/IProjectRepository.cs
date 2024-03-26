using Domain.OrganizationNS.Queries;
using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ProjectNS.Queries;

namespace Domain.ProjectNS.Interfaces;
public interface IProjectRepository
{

    Task<IEnumerable<Project>> Get(ProjectQuery query);
    Task<Project> Create(Project organization);
    Task<Project> Update(Project organization);
    Task Delete(Guid project);
    Task AddProjectDeveloper(ProjectDeveloper projectDeveloper);
    Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper);
    Task<ProjectFeedback?> GetProjectFeedback(ProjectFeedback projectFeedback);
    Task AddProjectFeedback(ProjectFeedback projectFeedback);
}
