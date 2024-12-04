using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.Finance
{
    public class ListFinancesUseCase(IFinanceRepository financeRepository)
    {
        public async Task<IEnumerable<Domain.Entities.Finance>> ExecuteAsync(int userId, string? type = null)
        {
            return await financeRepository.GetAllAsync(userId, type);
        }
    }
}