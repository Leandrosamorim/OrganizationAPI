using Domain.DeveloperNS;
using Domain.HttpService.Interfaces;
using Domain.OrganizationNS;
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

    public async Task<IEnumerable<dynamic>> Get(ProjectQuery query)
    {
        var project = await _projectRepository.Get(query);
        var developers = await _http.GetMyMatches(query.OrganizationId);

        var developerIdsInProjects = project.SelectMany(project => project.Developers.Select(dp => dp.DeveloperId)).Distinct();

        List<Developer> filteredDevelopers = developers.Where(developer => developerIdsInProjects.Contains(developer.UId)).ToList();

        var result = project.Select(x => new
        {
            UId = x.UId,
            Name = x.Name,
            Description = x.Description,
            Status = x.Status,
            OrganizationId = x.OrganizationId,
            Developers = filteredDevelopers
        });
        return result;

    }

    public async Task<IEnumerable<dynamic>> GetByDeveloperId(Guid developerId)
    {
        var project = await _projectRepository.Get(new ProjectQuery() { DeveloperId = developerId});

        var result = project.Select(x => new
        {
            UId = x.UId,
            Name = x.Name,
            Description = x.Description,
            Status = x.Status,
            OrganizationId = x.OrganizationId,
        });
        return result;

    }

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

    public async Task<bool> AddProjectFeedback(ProjectFeedback projectFeedback)
    {
        var myFeedback = await _projectRepository.GetProjectFeedback(projectFeedback);

        if (myFeedback == default(ProjectFeedback))
        {
            await _projectRepository.AddProjectFeedback(projectFeedback);
            return true;
        }
        
        return false;

    }

    public async Task RemoveProjectDeveloper(Guid organizationId, Guid projectId, Guid developerId)
    {
        var projectList = await _projectRepository.Get(new ProjectQuery() { ProjectId = projectId });
        var project = projectList.FirstOrDefault();

        if (project == null)
            throw new Exception("Project Not Found");
        else if (project.OrganizationId != organizationId)
            throw new Exception("Project does not belong to this organization");

        await _projectRepository.RemoveProjectDeveloper(new ProjectDeveloper()
        {
            DeveloperId = developerId,
            ProjectId = projectId
        });
    }

    public async Task<Project> Update(Project project)
    => await _projectRepository.Update(project);

    public async Task<ICollection<Developer>> GetDevelopersByProject(Guid organizationId, Guid projectId)
    {
        var myDevs = await _http.GetMyMatches(organizationId);
        var projectDevs = await _projectRepository.Get(new ProjectQuery() { ProjectId = projectId });
        var presentUids = projectDevs.SelectMany(x => x.Developers)
            .Select(x => x.DeveloperId)
            .ToList();

        var result = myDevs.Where(x => presentUids.Any(y => y == x.UId)).ToList();

        return result;
    }
}

