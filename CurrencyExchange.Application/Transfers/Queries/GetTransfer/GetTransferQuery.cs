using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Queries.GetTransfer;

public record GetTransferQuery(Guid TransferId) : IRequest<ErrorOr<Transfer>>;