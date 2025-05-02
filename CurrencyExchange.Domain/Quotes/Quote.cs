using CurrencyExchange.Domain.Common;
using ErrorOr;

namespace CurrencyExchange.Domain.Quotes;

public class Quote : Entity
{
    public SellCurrency SellCurrency { get; private set; }
    public BuyCurrency BuyCurrency { get; private set; }
    public decimal Amount { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public decimal InverseRate { get; private set; }
    public decimal ConvertedAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Quote(Guid? id = null) : base(id ?? Guid.NewGuid())
    {
    }

    public static ErrorOr<Quote> Create(SellCurrency sellCurrency, BuyCurrency buyCurrency, decimal amount, decimal exchangeRate)
    {
        if(exchangeRate <= 0)
        {
            return Error.Failure("InvalidExchangeRate", "Exchange rate must be greater than 0.");
        }

        return new Quote
        {
            SellCurrency = sellCurrency,
            BuyCurrency = buyCurrency,
            Amount = amount,
            ExchangeRate = exchangeRate,
            InverseRate = 1 / exchangeRate,
            ConvertedAmount = amount * exchangeRate,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public record ExchangeRate(decimal Rate, decimal InverseRate);

public enum SellCurrency
{
    AUD,
    USD,
    EUR
}

public enum BuyCurrency
{
    USD,
    INR,
    PHP
}