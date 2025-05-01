using CurrencyExchange.Domain.Quotes;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Queries.GetQuote;

public class GetQuoteQueryHandler : IRequestHandler<GetQuoteQuery, ErrorOr<Quote>>
{
    public Task<ErrorOr<Quote>> Handle(GetQuoteQuery request, CancellationToken cancellationToken) => new (() => new Quote());
}