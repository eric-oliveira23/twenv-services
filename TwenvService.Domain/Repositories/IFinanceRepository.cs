using TwenvService.Domain.Entities;

namespace TwenvService.Domain.Repositories;

public interface IFinanceRepository
{
    Task<IEnumerable<Finance>> GetAllAsync(int userId, string? type = null);
    Task<Finance?> GetByIdAsync(Guid id, int userId);
    Task CreateAsync(Finance finance);
    Task UpdateAsync(Finance finance);
    Task DeleteAsync(Guid id, int userId);
}