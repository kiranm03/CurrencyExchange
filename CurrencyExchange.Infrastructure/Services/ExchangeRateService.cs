using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;

namespace CurrencyExchange.Infrastructure.Services;

public class ExchangeRateService : IExchangeRateService
{
    
    public Task<ErrorOr<decimal>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency)
    {
        
    }
}