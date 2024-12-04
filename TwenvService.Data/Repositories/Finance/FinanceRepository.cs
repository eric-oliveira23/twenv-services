using Microsoft.EntityFrameworkCore;
using TwenvService.Domain.Repositories;

namespace TwenvService.Data.Repositories.Finance;

public class FinanceRepository(ApplicationDbContext dbContext) : IFinanceRepository
{
    public async Task<IEnumerable<Domain.Entities.Finance>> GetAllAsync(int userId, string? type = null)
    {
        var financesQuery = dbContext.Finances.Where(f => f.UserId == userId);

        if (string.IsNullOrEmpty(type)) return await financesQuery.ToListAsync();
        {
            type = type.ToLower();
            financesQuery = type switch
            {
                "income" => financesQuery.Where(f => !f.IsExpense),
                "expense" => financesQuery.Where(f => f.IsExpense),
                _ => financesQuery
            };
        }

        return await financesQuery.ToListAsync();
    }

    public async Task<Domain.Entities.Finance?> GetByIdAsync(Guid id, int userId)
    {
        return await dbContext.Finances
            .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
    }

    public async Task CreateAsync(Domain.Entities.Finance finance)
    {
        dbContext.Finances.Add(finance);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Finance finance)
    {
        var existingEntity = await dbContext.Finances
            .FirstOrDefaultAsync(f => f.Id == finance.Id && f.UserId == finance.UserId);

        if (existingEntity == null)
        {
            throw new InvalidOperationException($"Finance with ID {finance.Id} not found.");
        }

        existingEntity.Amount = finance.Amount;
        existingEntity.Description = finance.Description;
        existingEntity.IsExpense = finance.IsExpense;

        dbContext.Finances.Update(existingEntity);

        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(Guid id, int userId)
    {
        var finance = await GetByIdAsync(id, userId);

        if (finance == null)
        {
            throw new KeyNotFoundException("Finance not found or access denied.");
        }

        dbContext.Finances.Remove(finance);
        await dbContext.SaveChangesAsync();
    }
}