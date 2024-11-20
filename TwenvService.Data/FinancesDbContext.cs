using Microsoft.EntityFrameworkCore;
using TwenvService.Domain.Entities;

namespace TwenvService.Data
{
    public class FinancesDbContext(DbContextOptions<FinancesDbContext> options) : DbContext(options)
    {
        public DbSet<Finance> Finances { get; set; }
        public DbSet<User> Users { get; set; } // Add Users DbSet
    }
}