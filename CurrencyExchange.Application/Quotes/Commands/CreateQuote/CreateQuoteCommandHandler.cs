using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, ErrorOr<Quote>>
{
    public Task<ErrorOr<Quote>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken) => new(() => new Quote()); 
}