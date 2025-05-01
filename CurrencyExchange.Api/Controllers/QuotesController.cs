using CurrencyExchange.Application.Quotes.Commands.CreateQuote;
using CurrencyExchange.Application.Quotes.Queries.GetQuote;
using CurrencyExchange.Contracts.Quotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers;

[Route("transfers/[controller]")]
public class QuotesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteRequest request)
    {
        var createQuoteCommand = new CreateQuoteCommand(request.SellCurrency, request.BuyCurrency, request.Amount);
        
        var quote = await mediator.Send(createQuoteCommand);

        return Ok(quote);
    }
    
    [HttpGet("{quoteId}")]
    public async Task<IActionResult> GetQuote(Guid quoteId)
    {
        var getQuoteQuery = new GetQuoteQuery(quoteId);
        
        var quote = await mediator.Send(getQuoteQuery);

        return Ok(quote);
    }
}