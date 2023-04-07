using Domain.OrganizationNS;
using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(Organization organization)
        {
            try
            {
                await _organizationService.Create(organization);
                return Ok(organization);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [EnableCors("MatchAPI")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OrganizationQuery query)
        {
            try
            {
                var organizations = await _organizationService.Get(query);
                return Ok(organizations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [EnableCors("MatchAPI")]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var organizations = await _organizationService.GetAll();
                return Ok(organizations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(Organization organization)
        {
            try
            {
                var newOrganization = await _organizationService.Update(organization);
                return Ok(newOrganization);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid uid)
        {
            try
            {
                await _organizationService.Delete(uid);
                return Ok();
            }
            catch
            {
                return BadRequest($"Could not delete {uid}");
            }
        }
    }
}
