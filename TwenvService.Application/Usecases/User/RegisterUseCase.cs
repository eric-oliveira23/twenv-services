using TwenvService.Controllers.DTOs;
using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.User
{
    public class RegisterUseCase(IUserRepository userRepository)
    {
        public (bool Success, string Message, Domain.Entities.User? User) Execute(UserRegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return (false, "Passwords do not match.", null);
            }

            if (userRepository.UsernameExists(registerDto.Username))
            {
                return (false, "Username already exists.", null);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new Domain.Entities.User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash
            };

            userRepository.AddUser(user);

            return (true, "User registered successfully.", user);
        }
    }
}