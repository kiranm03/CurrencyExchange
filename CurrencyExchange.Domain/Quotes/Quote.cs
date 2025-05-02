using CurrencyExchange.Domain.Common;
using ErrorOr;

namespace CurrencyExchange.Domain.Quotes;

public class Quote : Entity
{
    public SellCurrency SellCurrency { get; private set; }
    public BuyCurrency BuyCurrency { get; private set; }
    public decimal Amount { get; private set; }
    public ExchangeRate ExchangeRate { get; private set; }
    public decimal InverseRate { get; private set; }
    public decimal ConvertedAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Quote(Guid? id = null) : base(id ?? Guid.NewGuid())
    {
    }

    public static Quote Create(SellCurrency sellCurrency, BuyCurrency buyCurrency, decimal amount, ExchangeRate exchangeRate) =>
        new()
        {
            SellCurrency = sellCurrency,
            BuyCurrency = buyCurrency,
            Amount = amount,
            ExchangeRate = exchangeRate,
            InverseRate = 1 / exchangeRate.Rate,
            ConvertedAmount = amount * exchangeRate.Rate,
            CreatedAt = DateTime.UtcNow
        };
}

public record ExchangeRate(decimal Rate)
{
    public static ErrorOr<ExchangeRate> Create(decimal rate)
    {
        if (rate <= 0)
            return Error.Failure("InvalidExchangeRate", "Exchange rate must be greater than 0.");

        return new ExchangeRate(rate);
    }
}

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