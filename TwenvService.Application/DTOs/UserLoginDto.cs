namespace TwenvService.Controllers.DTOs
{
    public class UserLoginDto(string username, string password)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }
}