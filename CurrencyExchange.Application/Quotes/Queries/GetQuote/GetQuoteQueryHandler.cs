using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Queries.GetQuote;

public class GetQuoteQueryHandler : IRequestHandler<GetQuoteQuery, ErrorOr<Quote>>
{
    public Task<ErrorOr<Quote>> Handle(GetQuoteQuery request, CancellationToken cancellationToken) => new (() => new Quote());
}