using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Queries.GetTransfer;

public class GetTransferQueryHandler(ITransferRepository transferRepository) : IRequestHandler<GetTransferQuery, ErrorOr<Transfer>>
{
    public async Task<ErrorOr<Transfer>> Handle(GetTransferQuery request, CancellationToken cancellationToken)
    {
        var transfer = await transferRepository.GetByIdAsync(request.TransferId, cancellationToken);
        if(transfer is null)
        {
            return Error.NotFound("TransferId", "Transfer not found.");
        }
        
        return transfer;
    }
}