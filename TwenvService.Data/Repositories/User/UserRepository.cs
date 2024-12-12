using TwenvService.Domain.Repositories;

namespace TwenvService.Data.Repositories.User
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public Domain.Entities.User? GetByUsername(string username)
        {
            return context.Users.SingleOrDefault(u => u.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return context.Users.Any(u => u.Username == username);
        }

        public void AddUser(Domain.Entities.User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}