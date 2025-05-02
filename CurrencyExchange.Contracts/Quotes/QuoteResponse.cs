namespace CurrencyExchange.Contracts.Quotes;

public record QuoteResponse(Guid QuoteId, decimal OfxRate, decimal InverseOfxRate, decimal ConvertedAmount);