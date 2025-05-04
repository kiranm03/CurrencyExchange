using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandHandler(IQuoteRepository quoteRepository, ITransferRepository transferRepository) 
    : IRequestHandler<CreateTransferCommand, ErrorOr<Transfer>>
{
    public async Task<ErrorOr<Transfer>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(request.QuoteId, cancellationToken);
        
        if(quote is null)
        {
            return Error.Validation("QuoteId", "Quote not found.");
        }
        
        var transfer = Transfer.Create(request.QuoteId, request.Payer, request.Recipient);
        
        await transferRepository.AddAsync(transfer, cancellationToken);
        
        return transfer;
    }
}