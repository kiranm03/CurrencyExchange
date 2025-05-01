using FluentValidation;

namespace CurrencyExchange.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(t => t.QuoteId)
            .NotEmpty()
            .WithMessage("Quote Id is required");
        
        RuleFor(t => t.Payer.Id)
            .NotEmpty()
            .WithMessage("Payer Id is required");

        RuleFor(t => t.Payer.Name)
            .NotEmpty()
            .WithMessage("Payer name is required");

        RuleFor(t => t.Payer.TransferReason)
            .NotEmpty()
            .WithMessage("Payer transfer reason is required");
        
        RuleFor(t => t.Recipient.Name)
            .NotEmpty()
            .WithMessage("Recipient name is required");
        
        RuleFor(t => t.Recipient.AccountNumber)
            .NotEmpty()
            .WithMessage("Recipient account number is required");
        
        RuleFor(t => t.Recipient.BankCode)
            .NotEmpty()
            .WithMessage("Recipient bank code is required");
        
        RuleFor(t => t.Recipient.BankName)
            .NotEmpty()
            .WithMessage("Recipient bank name is required");
    }
}