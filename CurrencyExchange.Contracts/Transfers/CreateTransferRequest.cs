namespace CurrencyExchange.Contracts.Transfers;

public record CreateTransferRequest(Guid QuoteId, Payer Payer, Recipient Recipient);