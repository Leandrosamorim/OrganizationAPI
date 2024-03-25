using Data.Context;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS;
using Domain.ProjectNS.Queries;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Project
            .Where(x => (query.DeveloperId != null && x.Developers.Any(y => y.DeveloperId == query.DeveloperId)) || (query.OrganizationId != null && x.OrganizationId == query.OrganizationId) || (query.ProjectId == x.UId))
            .Include(x => x.Developers)
            .Include(x => x.Feedbacks)
            .ToListAsync();
    }

    public async Task<Project> Update(Project project)
    {
        _context.Project.Update(project);
        await _context.SaveChangesAsync();
        return project;
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
        _context.ProjectDeveloper.Remove(projectDeveloper);
        await _context.SaveChangesAsync();
    }
}
