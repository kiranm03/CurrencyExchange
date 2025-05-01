using CurrencyExchange.Domain.Common;

namespace CurrencyExchange.Domain.Transfers;

public class Transfer : Entity
{
    public Transfer(Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        
    }
    
    public Guid QuoteId { get; private set; }
    public TransferStatus Status { get; private set; }
    public Payer Payer { get; private set; }
    public Recipient Recipient { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime EstimatedDeliveryDate { get; private set; }
}

public class Recipient
{
    public string Name { get; private set; }
    public string AccountNumber { get; private set; }
    public string BankCode { get; private set; }
    public string BankName { get; private set; }
}

public class Payer : Entity
{
    public Payer(Guid? id = null) : base(id ?? Guid.NewGuid()){ }
    public string Name { get; private set; }
    public string TransferReason { get; private set; }
}

public enum TransferStatus
{
    Created,
    Processing,
    Processed,
    Failed
}