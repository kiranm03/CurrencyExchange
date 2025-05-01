using System.Collections.Concurrent;
using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Domain.Transfers;

namespace CurrencyExchange.Infrastructure.Persistence;

public class InMemoryTransferRepository : ITransferRepository
{
    private readonly ConcurrentDictionary<Guid, Transfer> _transfers = new();


    public Task AddAsync(Transfer transfer, CancellationToken cancellationToken = default)
    {
        _transfers.AddOrUpdate(transfer.Id, transfer, (_, _) => transfer);
        return Task.CompletedTask;
    }

    public Task<Transfer?> GetByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
    {
        _transfers.TryGetValue(transferId, out var transfer);
        return Task.FromResult(transfer);
    }
}