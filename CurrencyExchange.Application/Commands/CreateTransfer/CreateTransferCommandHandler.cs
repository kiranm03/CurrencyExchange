using CurrencyExchange.Domain.Transfers;
using MediatR;

namespace CurrencyExchange.Application.Commands.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Transfer>
{
    public Task<Transfer> Handle(CreateTransferCommand request, CancellationToken cancellationToken) => Task.FromResult(new Transfer());
}