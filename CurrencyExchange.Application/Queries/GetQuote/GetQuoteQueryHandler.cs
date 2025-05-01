using CurrencyExchange.Domain.Quotes;
using MediatR;

namespace CurrencyExchange.Application.Queries.GetQuote;

public class GetQuoteQueryHandler : IRequestHandler<GetQuoteQuery, Quote>
{
    public Task<Quote> Handle(GetQuoteQuery request, CancellationToken cancellationToken) => Task.FromResult(new Quote());
}