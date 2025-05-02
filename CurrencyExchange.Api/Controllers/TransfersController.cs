using CurrencyExchange.Application.Transfers.Commands.CreateTransfer;
using CurrencyExchange.Application.Transfers.Queries.GetTransfer;
using CurrencyExchange.Contracts.Transfers;
using CurrencyExchange.Domain.Transfers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainPayerType = CurrencyExchange.Domain.Transfers.Payer;
using DomainRecipientType = CurrencyExchange.Domain.Transfers.Recipient;

using ContractsPayerType = CurrencyExchange.Contracts.Transfers.Payer;
using ContractsRecipientType = CurrencyExchange.Contracts.Transfers.Recipient;

namespace CurrencyExchange.Api.Controllers;

[Route("[controller]")]
public class TransfersController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferRequest request)
    {
        var payer = DomainPayerType.Create(request.Payer.Id, request.Payer.Name, request.Payer.TransferReason);
        var recipient = DomainRecipientType.Create(request.Recipient.Name, request.Recipient.AccountNumber,
            request.Recipient.BankCode, request.Recipient.BankName);
        var createTransferCommand = new CreateTransferCommand(request.QuoteId, payer, recipient);

        var result = await mediator.Send(createTransferCommand);

        return result.Match(
            transfer => CreatedAtAction(
                actionName: nameof(GetTransfer),
                routeValues: new { transferId = transfer.Id },
                value: ToDto(transfer)),
            Problem);
    }

    [HttpGet("{transferId}")]
    public async Task<IActionResult> GetTransfer(Guid transferId)
    {
        var getTransferQuery = new GetTransferQuery(transferId);

        var result = await mediator.Send(getTransferQuery);

        return result.Match(
            transfer => Ok(ToDto(transfer)),
            Problem);
    }

    private TransferResponse ToDto(Transfer transfer) =>
        new(transfer.Id, transfer.Status.ToString(), ToTransferDetailsDto(transfer),
            transfer.EstimatedDeliveryDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

    private TransferDetails ToTransferDetailsDto(Transfer transfer) =>
        new(transfer.QuoteId, ToDto(transfer.Payer), ToDto(transfer.Recipient));

    private ContractsPayerType ToDto(DomainPayerType payer) => new(payer.Id, payer.Name, payer.TransferReason);

    private ContractsRecipientType ToDto(DomainRecipientType recipient) =>
        new(recipient.Name, recipient.AccountNumber, recipient.BankCode, recipient.BankName);
}