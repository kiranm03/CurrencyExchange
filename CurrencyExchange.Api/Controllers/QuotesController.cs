using CurrencyExchange.Application.Quotes.Commands.CreateQuote;
using CurrencyExchange.Application.Quotes.Queries.GetQuote;
using CurrencyExchange.Contracts.Quotes;
using CurrencyExchange.Domain.Quotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers;

[Route("transfers/[controller]")]
public class QuotesController(IMediator mediator) : ApiController
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
    
    private QuoteResponse ToDto(Quote quote) => new(quote.Id, quote.Rate, quote.InverseRate, quote.ConvertedAmount);
}