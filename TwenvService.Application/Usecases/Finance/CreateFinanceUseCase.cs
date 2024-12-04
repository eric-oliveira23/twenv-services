using TwenvService.Domain.Repositories;

namespace TwenvService.Application.Usecases.Finance
{
    public class CreateFinanceUseCase(IFinanceRepository financeRepository)
    {
        public async Task ExecuteAsync(Domain.Entities.Finance finance)
        {
            await financeRepository.CreateAsync(finance);
        }
    }
}