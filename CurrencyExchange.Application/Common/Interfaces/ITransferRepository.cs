using CurrencyExchange.Domain.Transfers;

namespace CurrencyExchange.Application.Common.Interfaces;

public interface ITransferRepository
{
    Task AddAsync(Transfer transfer, CancellationToken cancellationToken = default);
    Task<Transfer?> GetByIdAsync(Guid transferId, CancellationToken cancellationToken = default);
}