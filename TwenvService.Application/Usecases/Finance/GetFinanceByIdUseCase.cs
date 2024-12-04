using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.Finance
{
    public class GetFinanceByIdUseCase(IFinanceRepository financeRepository)
    {
        public async Task<Domain.Entities.Finance?> ExecuteAsync(Guid id, int userId)
        {
            return await financeRepository.GetByIdAsync(id, userId);
        }
    }
}