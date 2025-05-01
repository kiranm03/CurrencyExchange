namespace CurrencyExchange.Contracts.Transfers;

public record TransferDetails(Guid QuoteId, Payer Payer, Recipient Recipient);