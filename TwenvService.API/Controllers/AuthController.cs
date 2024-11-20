using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TwenvService.API.DTOs;
using TwenvService.Data;
using TwenvService.Domain.Entities;

namespace TwenvService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(FinancesDbContext context, IConfiguration configuration) : ControllerBase
    {
        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            // Check if password matches
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            // Check if username already exists
            if (context.Users.Any(u => u.Username == registerDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Hash password before saving
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create the user
            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash
            };

            context.Users.Add(user);
            context.SaveChanges();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            // Find the user by username
            var user = context.Users.SingleOrDefault(u => u.Username == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Check if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

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
