using CurrencyExchange.Domain.Transfers;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, ErrorOr<Transfer>>
{
    public Task<ErrorOr<Transfer>> Handle(GetTransferQuery request, CancellationToken cancellationToken) => new (() => new Transfer());
}