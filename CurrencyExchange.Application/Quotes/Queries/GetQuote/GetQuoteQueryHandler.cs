using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Queries.GetQuote;

public class GetQuoteQueryHandler(IQuoteRepository quoteRepository) : IRequestHandler<GetQuoteQuery, ErrorOr<Quote>>
{
    public async Task<ErrorOr<Quote>> Handle(GetQuoteQuery request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.Id, cancellationToken);
        if (quote is null)
        {
            return Error.NotFound("Quote", "Quote not found.");
        }

        return quote;
    }
}