using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, ErrorOr<Quote>>
{
    public async Task<ErrorOr<Quote>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.SellCurrency, out SellCurrency sellCurrencyEnum))
        {
            return Error.Validation("SellCurrency", "SellCurrency is not supported");
        }

        if (!Enum.TryParse(request.BuyCurrency, out BuyCurrency buyCurrencyEnum))
        {
            return Error.Validation("BuyCurrency", "BuyCurrency is not supported");
        }
        
    }
}