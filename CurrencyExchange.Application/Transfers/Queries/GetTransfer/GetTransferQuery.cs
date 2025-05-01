using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Queries.GetTransfer;

public record GetTransferQuery : IRequest<ErrorOr<Transfer>>;