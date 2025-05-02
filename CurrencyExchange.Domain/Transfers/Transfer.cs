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

    public static Transfer Create(Guid quoteId, Payer payer, Recipient recipient)
        => new()
        {
            QuoteId = quoteId,
            Payer = payer, 
            Recipient = recipient,
            CreatedAt = DateTime.UtcNow,
            Status = TransferStatus.Processing,
            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(1)
        };
}

public record Recipient(string Name, string AccountNumber, string BankCode, string BankName)
{
    public static Recipient Create(string name, string accountNumber, string bankCode, string bankName) =>
        new(name, accountNumber, bankCode, bankName);
}

public class Payer : Entity
{
    private Payer(Guid id) : base(id) { }
    public string Name { get; private set; }
    public string TransferReason { get; private set; }

    public static Payer Create(Guid id, string name, string transferReason) =>
        new(id) { Name = name, TransferReason = transferReason };

}

public enum TransferStatus
{
    Created,
    Processing,
    Processed,
    Failed
}