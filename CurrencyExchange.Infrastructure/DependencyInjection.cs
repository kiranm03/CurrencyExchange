using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Infrastructure.Persistence;
using CurrencyExchange.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;

namespace CurrencyExchange.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalExchangeRatesApiOptions>(
            configuration.GetSection("ExternalExchangeRatesApi"));

        services.AddHttpClient("ExternalExchangeRatesApi", (serviceProvider, client) =>
            {
                var options = serviceProvider
                    .GetRequiredService<IOptions<ExternalExchangeRatesApiOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddResilienceHandler("ExternalExchangeRatesApi", builder =>
            {
                // Exponential backoff retries
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 4,
                    Delay = TimeSpan.FromSeconds(2),
                    BackoffType = DelayBackoffType.Exponential
                });

                // Timeout
                builder.AddTimeout(TimeSpan.FromSeconds(30));
            });

        services.AddSingleton<IQuoteRepository, InMemoryQuoteRepository>();
        services.AddSingleton<ITransferRepository, InMemoryTransferRepository>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        services.AddSingleton<IExchangeRateService, CachedExchangeRateService>(provider =>
        {
            var exchangeRateService = new ExchangeRateService(
                provider.GetRequiredService<IHttpClientFactory>(),
                provider.GetRequiredService<IOptions<ExternalExchangeRatesApiOptions>>());
            var cacheService = provider.GetRequiredService<ICacheService>();
            return new CachedExchangeRateService(exchangeRateService, cacheService);
        });
        services.Configure<ExternalExchangeRatesApiOptions>(
            configuration.GetSection("ExternalExchangeRatesApiOptions"));

        return services;
    }
}