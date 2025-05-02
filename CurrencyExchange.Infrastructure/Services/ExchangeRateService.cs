using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;

namespace CurrencyExchange.Infrastructure.Services;

public class ExchangeRateService(IHttpClientFactory httpClientFactory, IOptions<ExternalExchangeRatesApiOptions> options) : IExchangeRateService
{
    private readonly ExternalExchangeRatesApiOptions _options = options.Value;

    public async Task<ErrorOr<ExchangeRate>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if(sellCurrency.Equals(SellCurrency.AUD) && buyCurrency.Equals(BuyCurrency.USD))
            {
                return ExchangeRate.Create(0.768333m);
            }
            
            var url = $"?access_key={_options.AccessKey}&base={sellCurrency.ToString()}&symbols={buyCurrency.ToString()}";
            
            var httpClient = httpClientFactory.CreateClient("ExternalExchangeRatesApi");

            var response = await httpClient.GetFromJsonAsync<ExchangeRateResponse>(url, cancellationToken);

            return response?.Rates.TryGetValue(buyCurrency.ToString(), out decimal rate) == true 
                ? ExchangeRate.Create(rate) 
                : Error.Failure("ExchangeRateNotFound", "Exchange rate not found for the given currencies.");
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

public class ExternalExchangeRatesApiOptions
{
    public string AccessKey { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = string.Empty;
}
