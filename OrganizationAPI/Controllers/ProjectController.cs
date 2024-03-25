using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS;
using Domain.ProjectNS.Commands;
using Domain.ProjectNS.Queries;

namespace OrganizationAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : Controller
{

    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectCommand cmd)
    {
        var project = new Project()
        {
            Name = cmd.Name,
            Description = cmd.Description,
            Status = cmd.Status,
            OrganizationId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value)
        };
        try
        {
            await _projectService.Create(project);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]ProjectQuery query)
    {
        if(query.OrganizationId == Guid.Empty && query.DeveloperId == Guid.Empty)
        {
            query.OrganizationId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
        }
        try
        {
            var project = await _projectService.Get(query);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpPost("developer")]
    public async Task<IActionResult> AddDeveloper(Guid projectId, Guid developerId)
    {
        var projectList = await _projectService.Get(new ProjectQuery() { ProjectId = projectId });
        var project = projectList.FirstOrDefault();

        if (project.OrganizationId != Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value))
        {
            return Unauthorized("Cannot add a developer to a project you don`t own");
        }
        try
        {
            await _projectService.AddProjectDeveloper(developerId, projectId, project.OrganizationId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}


