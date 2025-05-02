namespace CurrencyExchange.Contracts.Transfers;

public record TransferResponse(Guid TransferId, string Status, TransferDetails TransferDetails, string EstimatedDeliveryDate);