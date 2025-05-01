using CurrencyExchange.Domain.Transfers;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, ErrorOr<Transfer>>
{
    public Task<ErrorOr<Transfer>> Handle(CreateTransferCommand request, CancellationToken cancellationToken) => new( () => new Transfer());
}