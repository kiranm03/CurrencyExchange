using MediatR;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public record GetTransferQuery : IRequest<Guid>;