using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IQuoteRepository, InMemoryQuoteRepository>();
        services.AddSingleton<ITransferRepository, InMemoryTransferRepository>();
        return services;
    }
}