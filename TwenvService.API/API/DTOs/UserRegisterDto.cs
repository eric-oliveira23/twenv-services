namespace TwenvService.API.DTOs
{
    public class UserRegisterDto(string username, string password, string confirmPassword)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
        public string ConfirmPassword { get; set; } = confirmPassword;
    }
}