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
    Task AddProjectDeveloper(ProjectDeveloper projectDeveloper);
    Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper);
}
