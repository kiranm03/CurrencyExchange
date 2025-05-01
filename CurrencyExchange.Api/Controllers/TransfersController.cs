using CurrencyExchange.Application.Commands.CreateTransfer;
using CurrencyExchange.Application.Queries.GetTransfer;
using CurrencyExchange.Contracts.Transfers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payer = CurrencyExchange.Domain.Transfers.Payer;
using Recipient = CurrencyExchange.Domain.Transfers.Recipient;

namespace CurrencyExchange.Api.Controllers;

[Route("[controller]")]
public class TransfersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferRequest request)
    {
        var createTransferCommand = new CreateTransferCommand(request.QuoteId, new Payer(), new Recipient());

        var transfer = await mediator.Send(createTransferCommand);

        return Ok(transfer);
    }

    [HttpGet("{transferId}")]
    public async Task<IActionResult> GetTransfer(Guid transferId)
    {
        var getTransferQuery = new GetTransferQuery();
        
        var transfer = await mediator.Send(getTransferQuery);

        return Ok(transfer);
    }
}