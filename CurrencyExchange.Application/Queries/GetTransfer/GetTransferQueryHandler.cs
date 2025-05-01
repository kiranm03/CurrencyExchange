using CurrencyExchange.Domain.Transfers;
using MediatR;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, Transfer>
{
    public Task<Transfer> Handle(GetTransferQuery request, CancellationToken cancellationToken) => Task.FromResult(new Transfer());
}