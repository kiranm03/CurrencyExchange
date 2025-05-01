using CurrencyExchange.Domain.Transfers;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public record GetTransferQuery : IRequest<ErrorOr<Transfer>>;