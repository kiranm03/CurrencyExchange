using CurrencyExchange.Application.Quotes.Commands.CreateQuote;
using FluentValidation.TestHelper;
using Xunit;

namespace CurrencyExchange.Application.Tests.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandValidatorTests
{
    private readonly CreateQuoteCommandValidator _validator = new();
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Amount_ShouldHaveValationError_WhenNotGreaterThanZero(decimal amount)
    {
        // Arrange
        var command = new CreateQuoteCommand("EUR", "USD", amount);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Amount)
            .WithErrorMessage("Amount must be greater than 0");
    }
}