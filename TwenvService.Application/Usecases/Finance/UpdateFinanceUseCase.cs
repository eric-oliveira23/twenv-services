using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.Finance
{
    public class UpdateFinanceUseCase(IFinanceRepository financeRepository)
    {
        public async Task ExecuteAsync(Domain.Entities.Finance updatedFinance)
        {
            await financeRepository.UpdateAsync(updatedFinance);
        }
    }
}