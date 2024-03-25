using Domain.HttpService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMatchHttpService _matchHttpService;

        public MatchController(IMatchHttpService matchHttpService)
        {
            _matchHttpService = matchHttpService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyMatches()
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var developers = await _matchHttpService.GetMyMatches(userUId);
                return Ok(developers);
            }
            catch
            {
                return NoContent();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MatchDeveloper(Guid developerUid)
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var match = await _matchHttpService.MatchDeveloper(developerUid, userUId);
                return Ok(match);
            }
            catch { return NoContent(); }
        }

        [Authorize]
        [Route("developers")]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationsToMatch(int stackId)
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var organizations = await _matchHttpService.GetDevelopersToMatch(userUId, stackId);
                return Ok(organizations);
            }
            catch { return NoContent(); }
        }
    }
}

