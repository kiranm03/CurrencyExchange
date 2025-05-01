using CurrencyExchange.Domain.Transfers;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Commands.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, ErrorOr<Transfer>>
{
    public Task<ErrorOr<Transfer>> Handle(CreateTransferCommand request, CancellationToken cancellationToken) => new( () => new Transfer());
}