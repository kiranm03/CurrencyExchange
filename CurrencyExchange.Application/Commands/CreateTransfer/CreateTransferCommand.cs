using CurrencyExchange.Domain.Transfers;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Commands.CreateTransfer;

public record CreateTransferCommand(
    Guid QuoteId,
    Payer Payer,
    Recipient Recipient) : IRequest<ErrorOr<Transfer>>;