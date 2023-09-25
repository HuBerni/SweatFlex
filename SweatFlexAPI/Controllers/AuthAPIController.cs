using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
using SweatFlexData.Interface;
using SweatFlexEF.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private IConfiguration _configuration;
        private ApiResponse _response;

        public AuthAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _response = new();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login([FromBody]LoginDTO dto)
        {
            try
            {
                var userDto = await _dataHandler.LoginAsync(dto.Email, dto.Password);

                if (userDto == null)
                {
                    return BadRequest("Invalid username or password");
                }

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDto.Id),
                    new Claim(ClaimTypes.Role, userDto.Role.ToString()),
                };

                string token = GenerateToken(authClaims);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = token;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error logging in: {ex.Message}");
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
