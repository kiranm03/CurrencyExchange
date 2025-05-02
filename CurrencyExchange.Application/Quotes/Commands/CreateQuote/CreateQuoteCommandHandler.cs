using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandler(IExchangeRateService exchangeRateService, IQuoteRepository quoteRepository) : IRequestHandler<CreateQuoteCommand, ErrorOr<Quote>>
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
        
        var exchangeRate = await exchangeRateService.GetExchangeRate(sellCurrencyEnum, buyCurrencyEnum, cancellationToken);
        
        if (exchangeRate.IsError)
        {
            return exchangeRate.Errors;
        }
        
        var quoteResult = Quote.Create(sellCurrencyEnum, buyCurrencyEnum, request.Amount, exchangeRate.Value);
        
        if (quoteResult.IsError)
        {
            return quoteResult.Errors;
        }
        
        var quote = quoteResult.Value;
        
        await quoteRepository.AddAsync(quote, cancellationToken);
        
        return quote;
    }
}