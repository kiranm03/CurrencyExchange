using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;

namespace CurrencyExchange.Infrastructure.Services;

internal class ExchangeRateService(IHttpClientFactory httpClientFactory, IOptions<ExternalExchangeRatesApiOptions> options) : IExchangeRateService
{
    private readonly ExternalExchangeRatesApiOptions _options = options.Value;

    public async Task<ErrorOr<decimal>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency, CancellationToken cancellationToken = default)
    {
        var url = $"?access_key={_options.AccessKey}&base={sellCurrency.ToString()}&symbols={buyCurrency.ToString()}";

        try
        {
            var httpClient = httpClientFactory.CreateClient("ExternalExchangeRatesApi");

            var response = await httpClient.GetFromJsonAsync<ExchangeRateResponse>(url, cancellationToken);

            if (response?.Rates.TryGetValue(buyCurrency.ToString(), out decimal rate) == true)
            {
                return rate;
            }

            return Error.Failure("ExchangeRateNotFound", "Exchange rate not found for the given currencies.");
        }
        catch (Exception ex)
        {
            return Error.Failure("ApiError", ex.Message);
        }
    }

    private class ExchangeRateResponse
    {
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}

public class CachedExchangeRateService(IExchangeRateService innerService, IMemoryCache cache) : IExchangeRateService
{
    public async Task<ErrorOr<decimal>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{sellCurrency}_{buyCurrency}";

        if (cache.TryGetValue(cacheKey, out decimal cachedRate))
        {
            return cachedRate;
        }

        var rateResult = await innerService.GetExchangeRate(sellCurrency, buyCurrency, cancellationToken);

        if (rateResult.IsError)
        {
            return rateResult;
        }

        cache.Set(cacheKey, rateResult.Value, TimeSpan.FromMinutes(30));
        return rateResult.Value;
    }
}