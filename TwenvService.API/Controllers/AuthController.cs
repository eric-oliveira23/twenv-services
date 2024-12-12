using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwenvService.Application.Usecases.User;
using TwenvService.Controllers.DTOs;
using TwenvService.Domain.Entities;
namespace TwenvService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        RegisterUseCase registerUserUseCase,
        LoginUseCase loginUseCase,
        IConfiguration configuration)
        : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            var (success, message, user) = registerUserUseCase.Execute(registerDto);

            if (!success)
            {
                return BadRequest(message);
            }

            var token = GenerateJwtToken(user!);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var (success, message, user) = loginUseCase.Execute(loginDto);

            if (!success)
            {
                return Unauthorized(message);
            }

            var token = GenerateJwtToken(user!);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
