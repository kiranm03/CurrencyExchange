using CurrencyExchange.Domain.Quotes;

namespace CurrencyExchange.Application.Common.Interfaces;

public interface IQuoteRepository
{
    Task AddAsync(Quote quote, CancellationToken cancellationToken = default);
    Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}