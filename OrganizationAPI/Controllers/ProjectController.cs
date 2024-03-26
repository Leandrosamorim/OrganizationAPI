using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS;
using Domain.ProjectNS.Commands;
using Domain.ProjectNS.Queries;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Cors;

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
        if(query.OrganizationId == Guid.Empty)
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
    [HttpGet("developerProjects")]
    public async Task<IActionResult> GetByDeveloperId([FromQuery] Guid developerId)
    {
        try
        {
            var project = await _projectService.GetByDeveloperId(developerId);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(Project project)
    {
        try
        {
            var result = await _projectService.Update(project);
            return Ok(result);
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
        var projectList = await _projectService.Get(new ProjectQuery() { ProjectId = projectId, OrganizationId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value) });
        var project = projectList.FirstOrDefault();
        var type = project.GetType();
        var prop = type.GetProperty("OrganizationId");
        var organizationId = prop.GetValue(project);

        try
        {
            await _projectService.AddProjectDeveloper(developerId, projectId, organizationId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpGet($"developer")]
    public async Task<IActionResult> GetProjectDevelopers([FromQuery]Guid projectId)
    {
        var organizationId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
        try
        {
            var devs = await _projectService.GetDevelopersByProject(organizationId, projectId);
            return Ok(devs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpDelete($"developer")]
    public async Task<IActionResult> DeleteProjectDeveloper([FromQuery] Guid projectId, Guid developerId)
    {
        var organizationId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
        try
        {
            await _projectService.RemoveProjectDeveloper(organizationId, projectId, developerId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [EnableCors("DeveloperApi")]
    [HttpPost($"feedback")]
    public async Task<IActionResult> AddProjectFeedback([FromBody] ProjectFeedback projectFeedback)
    {
        try
        {
            await _projectService.AddProjectFeedback(projectFeedback);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}


