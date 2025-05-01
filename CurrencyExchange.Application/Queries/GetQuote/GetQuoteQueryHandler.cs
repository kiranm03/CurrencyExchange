using MediatR;

namespace CurrencyExchange.Application.Queries.GetQuote;

public class GetQuoteQueryHandler : IRequestHandler<GetQuoteQuery, Guid>
{
    public Task<Guid> Handle(GetQuoteQuery request, CancellationToken cancellationToken) => Task.FromResult(Guid.NewGuid());
}