using CurrencyExchange.Application.Quotes.Commands.CreateQuote;
using CurrencyExchange.Application.Quotes.Queries.GetQuote;
using CurrencyExchange.Contracts.Quotes;
using CurrencyExchange.Domain.Quotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers;

[Route("transfers/quote")]
public class QuotesController(IMediator mediator, ILogger<QuotesController> logger) : ApiController(logger)
{
    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequest request)
    {
        var createQuoteCommand = new CreateQuoteCommand(request.SellCurrency, request.BuyCurrency, request.Amount);

        var result = await mediator.Send(createQuoteCommand);

        return result.Match(
            quote => CreatedAtAction(
                actionName: nameof(GetQuote), 
                routeValues:new { quoteId = quote.Id }, 
                value:ToDto(quote)),
            Problem);
    }

    [HttpGet("{quoteId}")]
    public async Task<IActionResult> GetQuote(Guid quoteId)
    {
        var getQuoteQuery = new GetQuoteQuery(quoteId);
        
        var result = await mediator.Send(getQuoteQuery);

        return result.Match(
            quote => Ok(ToDto(quote)),
            Problem);
    }
    
    private QuoteResponse ToDto(Quote quote)
    {
        return new QuoteResponse(
            quote.Id,
            Math.Round(quote.ExchangeRate.Rate, 6),
            Math.Round(quote.InverseRate, 5),
            Math.Round(quote.ConvertedAmount, 2)
        );
    }
}