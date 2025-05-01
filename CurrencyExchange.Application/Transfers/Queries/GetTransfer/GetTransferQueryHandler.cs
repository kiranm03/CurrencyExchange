using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Queries.GetTransfer;

public class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, ErrorOr<Transfer>>
{
    public Task<ErrorOr<Transfer>> Handle(GetTransferQuery request, CancellationToken cancellationToken) => new (() => new Transfer());
}