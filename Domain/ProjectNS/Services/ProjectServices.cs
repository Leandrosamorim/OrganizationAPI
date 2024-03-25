using Domain.DeveloperNS;
using Domain.HttpService.Interfaces;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS.Queries;
using System.Xml;

namespace Domain.ProjectNS.Services;
public class ProjectServices : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMatchHttpService _http;

    public ProjectServices(IProjectRepository projectRepository, IMatchHttpService http)
    {
        _projectRepository = projectRepository;
        _http = http;
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

    public async Task<bool> AddProjectDeveloper(Guid developerId, Guid projectId, Guid organizationId)
    {
        var myDevs = await _http.GetMyMatches(organizationId);

        if (myDevs != null && !myDevs.Any(x => x.UId == developerId))
        {
            return false;
        }
        await _projectRepository.AddProjectDeveloper(new ProjectDeveloper()
        {
            DeveloperId = developerId,
            ProjectId = projectId
        });
        return true;

    }

    public async Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper)
        => await _projectRepository.RemoveProjectDeveloper(projectDeveloper);

    public async Task<Project> Update(Project organization)
    => await _projectRepository.Update(organization);

    async Task IProjectService.Delete(Guid project)
    => await _projectRepository.Delete(project);
}

