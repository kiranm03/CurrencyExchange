using CurrencyExchange.Contracts.Quotes;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers;

[Route("transfers/[controller]")]
public class QuotesController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteRequest request)
    {
        return Ok();
    }
    
    [HttpGet("{quoteId}")]
    public IActionResult GetQuote(Guid quoteId)
    {
        return Ok();
    }
}