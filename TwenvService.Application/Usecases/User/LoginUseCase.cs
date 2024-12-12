using TwenvService.Controllers.DTOs;
using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.User
{
    public class LoginUseCase(IUserRepository userRepository)
    {
        public (bool Success, string Message, Domain.Entities.User? User) Execute(UserLoginDto loginDto)
        {
            var user = userRepository.GetByUsername(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return (false, "Invalid username or password.", null);
            }

            return (true, "Login successful.", user);
        }
    }
}