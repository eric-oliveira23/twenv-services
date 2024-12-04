using Microsoft.EntityFrameworkCore;
using TwenvService.Domain.Entities;

namespace TwenvService.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Finance> Finances { get; init; }
        public DbSet<User> Users { get; init; }
    }
}