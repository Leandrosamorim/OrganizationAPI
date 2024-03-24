using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS;

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
    public async Task<IActionResult> Create(Project project)
    {
        if (project.OrganizationId != Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value))
        {
            return Unauthorized("Cannot create project for another organization");
        }
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
    }
