using CurrencyExchange.Contracts.Transfers;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers;

[Route("[controller]")]
public class TransfersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateTransfer([FromBody] CreateTransferRequest request)
    {
        return Ok();
    }

    [HttpGet("{transferId}")]
    public IActionResult GetTransfer(Guid transferId)
    {
        return Ok();
    }
}