using CurrencyExchange.Domain.Quotes;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Commands.CreateQuote;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, ErrorOr<Quote>>
{
    public Task<ErrorOr<Quote>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken) => new(() => new Quote()); 
}