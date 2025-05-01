using MediatR;

namespace CurrencyExchange.Application.Queries.GetTransfer;

public class GetTransferQueryHandler : IRequestHandler<GetTransferQuery, Guid>
{
    public Task<Guid> Handle(GetTransferQuery request, CancellationToken cancellationToken) => Task.FromResult(Guid.NewGuid());
}