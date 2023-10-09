using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.Enum;
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

        public AuthAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO dto)
        {
            ApiResponse<UserLoggedInDTO> response = new();

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
                    new Claim(ClaimTypes.Role, ((RoleEnum)userDto.Role).ToString()),
                };                

                string token = GenerateToken(authClaims);

                response.StatusCode = HttpStatusCode.OK;
                response.Result = new UserLoggedInDTO()
                {
                    Coach = userDto.Coach,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    Id = userDto.Id,
                    LastName = userDto.LastName,
                    Role = userDto.Role,
                    Token = token

                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error logging in: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Register([FromBody] UserCreateDTO dto)
        {
            ApiResponse<UserLoggedInDTO> response = new();

            try
            {
                var userDto = await _dataHandler.CreateUserAsync(dto);

                if (userDto == null)
                {
                    return BadRequest("User could not be created");
                }

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDto.Id),
                    new Claim(ClaimTypes.Role, ((RoleEnum)userDto.Role).ToString()),
                };

                string token = GenerateToken(authClaims);

                response.StatusCode = HttpStatusCode.OK;
                response.Result = new UserLoggedInDTO()
                {
                    Coach = userDto.Coach,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    Id = userDto.Id,
                    LastName = userDto.LastName,
                    Role = userDto.Role,
                    Token = token

                };
                return Ok(response);
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
