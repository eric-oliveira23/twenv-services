using Microsoft.Extensions.DependencyInjection;
using TwenvService.Data.Repositories;
using TwenvService.Data.Repositories.Finance;
using TwenvService.Domain.Repositories;

namespace TwenvService.Data.Extensions
{
    public static class DataServiceExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IFinanceRepository, FinanceRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}