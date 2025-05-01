using System.Collections.Concurrent;
using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;

namespace CurrencyExchange.Infrastructure.Persistence;

public class InMemoryQuoteRepository : IQuoteRepository
{
    private readonly ConcurrentDictionary<Guid, Quote> _quotes = new();
    public Task AddAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _quotes.AddOrUpdate(quote.Id, quote, (_, _) => quote);
        return Task.CompletedTask;
    }

    public Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _quotes.TryGetValue(id, out var quote);
        return Task.FromResult(quote);
    }
}