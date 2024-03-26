using Data.Context;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS;
using Domain.ProjectNS.Queries;
using Microsoft.EntityFrameworkCore;
using Domain.OrganizationNS;

namespace Data.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly OrganizationDBContext _context;

    public ProjectRepository(OrganizationDBContext context)
    {
        _context = context;
    }

    public async Task<Project> Create(Project project)
    {
        await _context.AddAsync(project);
        _context.SaveChanges();
        return project;
    }


    public async Task<IEnumerable<Project>> Get(ProjectQuery query)
    {
        if (query.ProjectId != Guid.Empty)
            return await _context.Project.Where(x => x.UId == query.ProjectId)
                .Include(x => x.Developers)
                .Include(x => x.Feedbacks)
                .ToListAsync();
        else
        return await _context.Project
            .Where(x => (query.DeveloperId != Guid.Empty && x.Developers.Any(y => y.DeveloperId == query.DeveloperId)) || (query.OrganizationId != Guid.Empty && x.OrganizationId == query.OrganizationId))
            .Include(x => x.Developers)
            .Include(x => x.Feedbacks)
            .ToListAsync();
    }

    public async Task<Project> Update(Project project)
    {
        var projectModel = new
        {
            project.Name,
            project.Description,
            project.Status,
        };
        var projectToUpdate = _context.Project.FirstOrDefault(x => x.UId == project.UId);

        _context.Project.Entry(projectToUpdate).CurrentValues.SetValues(projectModel);
        await _context.SaveChangesAsync();
        return projectToUpdate;
    }

    public async Task Delete(Guid projectId)
    {
        var project = await _context.Project.Where(x => x.UId == projectId).FirstOrDefaultAsync();
        _context.Project.Remove(project);
        await _context.SaveChangesAsync();
    }

    public async Task AddProjectDeveloper(ProjectDeveloper projectDeveloper)
    {
        _context.ProjectDeveloper.Add(projectDeveloper);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveProjectDeveloper(ProjectDeveloper projectDeveloper)
    {
        var projectDeveloperToRemove = _context.ProjectDeveloper.Where(x =>
        x.ProjectId == projectDeveloper.ProjectId &&
        x.DeveloperId == projectDeveloper.DeveloperId).FirstOrDefault();
        _context.ProjectDeveloper.Remove(projectDeveloperToRemove);
        await _context.SaveChangesAsync();
    }
    public async Task<ProjectFeedback?> GetProjectFeedback(ProjectFeedback projectFeedback)
    {
        return _context.ProjectFeedback.Where(x => x.ProjectUId == projectFeedback.ProjectUId && x.DeveloperUId == projectFeedback.DeveloperUId).FirstOrDefault();
    }

    public async Task AddProjectFeedback(ProjectFeedback projectFeedback)
    {
        _context.ProjectFeedback.Add(projectFeedback);
        await _context.SaveChangesAsync();
    }
}
