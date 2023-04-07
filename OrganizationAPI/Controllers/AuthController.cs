using Domain.AuthNS;
using Domain.OrganizationNS;
using Domain.OrganizationNS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        private IOrganizationService _organizationService;

        public AuthController(IConfiguration configuration, IOrganizationService organizationService)
        {
            _configuration = configuration;
            _organizationService = organizationService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(string username, string password)
        {
            if (String.IsNullOrEmpty(password) && String.IsNullOrEmpty(password))
                return BadRequest("Invalid User");
            else
            {
                var userData = await GetOrganization(username, password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UId", userData.UId.ToString(), null),
                        new Claim("Username", userData.Login ),
                        new Claim("Password", userData.Password)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                    return BadRequest("User not found");
            }
        }
        [HttpGet]
        public async Task<Organization> GetOrganization(string login, string password)
        {
            return await _organizationService.Login(login, password);
        }
    }
}
