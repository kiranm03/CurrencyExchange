using CurrencyExchange.Domain.Quotes;
using ErrorOr;

namespace CurrencyExchange.Application.Common.Interfaces;

public interface IExchangeRateService
{
    Task<ErrorOr<decimal>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency);
}