using TwenvService.Domain.Entities;

namespace TwenvService.Domain.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        bool UsernameExists(string username);
        void AddUser(User user);
    }
}