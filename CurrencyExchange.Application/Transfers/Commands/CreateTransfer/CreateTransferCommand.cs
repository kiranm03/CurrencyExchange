using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Commands.CreateTransfer;

public record CreateTransferCommand(
    Guid QuoteId,
    Payer Payer,
    Recipient Recipient) : IRequest<ErrorOr<Transfer>>;