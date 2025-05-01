using CurrencyExchange.Domain.Transfers;
using MediatR;

namespace CurrencyExchange.Application.Commands.CreateTransfer;

public record CreateTransferCommand(
    Guid QuoteId,
    Payer Payer,
    Recipient Recipient) : IRequest<Transfer>;