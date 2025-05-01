using CurrencyExchange.Domain.Quotes;
using MediatR;

namespace CurrencyExchange.Application.Commands.CreateQuote;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, Quote>
{
    public Task<Quote> Handle(CreateQuoteCommand request, CancellationToken cancellationToken) => Task.FromResult(new Quote());
}