using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using Microsoft.Extensions.Caching.Memory;

namespace CurrencyExchange.Infrastructure.Services;

public class CachedExchangeRateService(ExchangeRateService exchangeRateService, ICacheService cacheService) : IExchangeRateService
{
    public async Task<ErrorOr<ExchangeRate>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{sellCurrency}_{buyCurrency}";
        
        return await cacheService.GetOrSetAsync(
            cacheKey,
            () => exchangeRateService.GetExchangeRate(sellCurrency, buyCurrency, cancellationToken),
            TimeSpan.FromMinutes(30));
    }
}

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? absoluteExpiration = null);
}

public class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? absoluteExpiration = null)
    {
        if (cache.TryGetValue(cacheKey, out T value))
        {
            return value;
        }

        value = await factory();

        cache.Set(cacheKey, value, absoluteExpiration ?? TimeSpan.FromMinutes(30));

        return value;
    }
}
