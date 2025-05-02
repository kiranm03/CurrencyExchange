using CurrencyExchange.Application.Transfers.Commands.CreateTransfer;
using CurrencyExchange.Domain.Transfers;
using FluentValidation.TestHelper;
using Xunit;

namespace CurrencyExchange.Application.Tests.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandValidatorTests
{
    private readonly CreateTransferCommandValidator _validator = new();
    
    [Theory]
    [InlineData(null)]
    public void QuoteId_ShouldHaveValationError_WhenNullOrEmpty(Guid quoteId)
    {
        // Arrange
        var payer = Payer.Create(Guid.NewGuid(), "Payer Name", "Transfer Reason");
        var recipient = Recipient.Create("Recipient Name", "Account Number", "Bank Code", "Bank Name");
        var command = new CreateTransferCommand(quoteId, payer, recipient);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(q => q.QuoteId)
            .WithErrorMessage("Quote Id is required");
    }
    
}