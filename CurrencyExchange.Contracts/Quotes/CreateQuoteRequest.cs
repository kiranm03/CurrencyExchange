namespace CurrencyExchange.Contracts.Quotes;

public record CreateQuoteRequest(string SellCurrency, string BuyCurrency, decimal Amount);