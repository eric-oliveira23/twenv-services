﻿using Microsoft.Extensions.DependencyInjection;
using TwenvService.Application.Usecases;
using TwenvService.Application.Usecases.Finance;

namespace TwenvService.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<CreateFinanceUseCase>();
            services.AddTransient<ListFinancesUseCase>();
            services.AddTransient<UpdateFinanceUseCase>();
            services.AddTransient<DeleteFinanceUseCase>();
            services.AddTransient<GetFinanceByIdUseCase>();
            return services;
        }
    }
}