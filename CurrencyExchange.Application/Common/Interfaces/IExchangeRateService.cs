using CurrencyExchange.Domain.Quotes;
using ErrorOr;

namespace CurrencyExchange.Application.Common.Interfaces;

public interface IExchangeRateService
{
    Task<ErrorOr<ExchangeRate>> GetExchangeRate(SellCurrency sellCurrency, BuyCurrency buyCurrency,
        CancellationToken cancellationToken = default);
}