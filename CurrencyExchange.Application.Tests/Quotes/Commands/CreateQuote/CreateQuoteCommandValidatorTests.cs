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
    
    [Theory]
    [InlineData(1)]
    [InlineData(162)]
    [InlineData(999.99)]
    public void Amount_ShouldNotHaveValationError_WhenGreaterThanZero(decimal amount)
    {
        // Arrange
        var command = new CreateQuoteCommand("EUR", "USD", amount);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.Amount);
    }
    
    [Theory]
    [InlineData("USD", "USD")]
    [InlineData( "EUR", "EUR")]
    public void SellCurrency_ShouldHaveValationError_WhenEqualToBuyCurrency(string sellCurrency, string buyCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.SellCurrency)
            .WithErrorMessage("SellCurrency and BuyCurrency must be different");
    }
    
    [Theory]
    [InlineData("USD", "EUR")]
    [InlineData( "EUR", "USD")]
    public void SellCurrency_ShouldNotHaveValationError_WhenNotEqualToBuyCurrency(string sellCurrency, string buyCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.SellCurrency);
    }
    
    [Theory]
    [InlineData("USD", "USD")]
    [InlineData( "EUR", "EUR")]
    public void BuyCurrency_ShouldHaveValationError_WhenEqualToSellCurrency(string sellCurrency, string buyCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.BuyCurrency)
            .WithErrorMessage("SellCurrency and BuyCurrency must be different");
    }
    
    [Theory]
    [InlineData("USD", "EUR")]
    [InlineData( "EUR", "USD")]
    public void BuyCurrency_ShouldNotHaveValationError_WhenNotEqualToSellCurrency(string sellCurrency, string buyCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.BuyCurrency);
    }
    
    [Theory]
    [InlineData("USD", "EUR", 1)]
    [InlineData( "EUR", "USD", 1)]
    public void SellCurrency_ShouldNotHaveValationError_WhenValid(string sellCurrency, string buyCurrency, decimal amount)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, amount);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.SellCurrency);
    }
    
    [Theory]
    [InlineData("USD", "EUR", 1)]
    [InlineData( "EUR", "USD", 1)]
    public void BuyCurrency_ShouldNotHaveValationError_WhenValid(string sellCurrency, string buyCurrency, decimal amount)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, buyCurrency, amount);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.BuyCurrency);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData( null)]
    [InlineData( "USDA")]
    [InlineData( "123")]
    public void SellCurrency_ShouldHaveValationError_WhenNullOrEmpty(string sellCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand(sellCurrency, "AUD", 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.SellCurrency)
            .WithErrorMessage("SellCurrency must be a 3 letter currency code");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData( null)]
    [InlineData( "USDA")]
    [InlineData( "123")]
    public void BuyCurrency_ShouldHaveValationError_WhenNullOrEmpty(string buyCurrency)
    {
        // Arrange
        var command = new CreateQuoteCommand("USD", buyCurrency, 1);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.BuyCurrency)
            .WithErrorMessage("BuyCurrency must be a 3 letter currency code");
    }
    
}