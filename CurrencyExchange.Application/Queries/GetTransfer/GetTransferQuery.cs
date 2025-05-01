using CurrencyExchange.Domain.Transfers;
using MediatR;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public record GetTransferQuery : IRequest<Transfer>;