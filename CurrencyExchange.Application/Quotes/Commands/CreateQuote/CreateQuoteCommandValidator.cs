using FluentValidation;

namespace CurrencyExchange.Application.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
{
    public CreateQuoteCommandValidator()
    {
        RuleFor(q=> q.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        
        RuleFor(q=> q.SellCurrency)
            .NotEmpty().WithMessage("SellCurrency must be a 3 letter currency code")
            .Length(3)
            .Matches(@"^[A-Z]{3}$")
            .WithMessage("SellCurrency must be a 3 letter currency code")
            .NotEqual(q => q.BuyCurrency)
            .WithMessage("SellCurrency and BuyCurrency must be different");
        
        RuleFor(q=> q.BuyCurrency)
            .NotEmpty().WithMessage("BuyCurrency must be a 3 letter currency code")
            .Length(3)
            .Matches(@"^[A-Z]{3}$")
            .WithMessage("BuyCurrency must be a 3 letter currency code")
            .NotEqual(q => q.SellCurrency)
            .WithMessage("SellCurrency and BuyCurrency must be different");
    }
    
}