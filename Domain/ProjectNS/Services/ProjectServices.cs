using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS.Queries;

namespace Domain.ProjectNS.Services;
public class ProjectServices : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectServices(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Create(Project project)
        => await _projectRepository.Create(project);

    public async Task<bool> Delete(Guid projectUid)
    {
        try
        {
            await _projectRepository.Delete(projectUid);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<IEnumerable<Project>> Get(ProjectQuery query)
        => await _projectRepository.Get(query);

    public async Task AddProjectDeveloper(ProjectDeveloper projectDeveloper)
        => await _projectRepository.AddProjectDeveloper(projectDeveloper);

    public async Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper)
        => await _projectRepository.RemoveProjectDeveloper(projectDeveloper);

    public async Task<Project> Update(Project organization)
    => await _projectRepository.Update(organization);

    async Task IProjectService.Delete(Guid project)
    => await _projectRepository.Delete(project);
}

