using CurrencyExchange.Domain.Common;

namespace CurrencyExchange.Domain.Quotes;

public class Quote : Entity
{
    public string SellCurrency { get; private set; }
    public string BuyCurrency { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Rate { get; private set; }
    public decimal InverseRate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Quote(Guid? id = null) : base(id ?? Guid.NewGuid())
    {
    }
}