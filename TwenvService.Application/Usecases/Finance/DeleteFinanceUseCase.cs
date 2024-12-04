using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.Finance
{
    public class DeleteFinanceUseCase(IFinanceRepository financeRepository)
    {
        public async Task ExecuteAsync(Guid id, int userId)
        {
            await financeRepository.DeleteAsync(id, userId);
        }
    }
}