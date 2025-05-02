using FluentValidation;

namespace CurrencyExchange.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(t => t.QuoteId)
            .NotEmpty()
            .WithMessage("Quote Id is required");

        RuleFor(t => t.Payer)
            .NotNull()
            .WithMessage("Payer is required")
            .ChildRules(p =>
            {
                p.RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Payer Id is required");

                p.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Payer name is required");

                p.RuleFor(x => x.TransferReason)
                    .NotEmpty()
                    .WithMessage("Payer transfer reason is required");
            });
        
        RuleFor(t => t.Recipient)
            .NotNull()
            .WithMessage("Recipient is required")
            .ChildRules(r =>
            {
                r.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Recipient name is required");

                r.RuleFor(x => x.AccountNumber)
                    .NotEmpty()
                    .WithMessage("Recipient account number is required");

                r.RuleFor(x => x.BankCode)
                    .NotEmpty()
                    .WithMessage("Recipient bank code is required");

                r.RuleFor(x => x.BankName)
                    .NotEmpty()
                    .WithMessage("Recipient bank name is required");
            });
    }
}