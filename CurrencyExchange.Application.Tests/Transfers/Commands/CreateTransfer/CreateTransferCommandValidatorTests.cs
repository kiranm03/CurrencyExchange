using CurrencyExchange.Application.Transfers.Commands.CreateTransfer;
using CurrencyExchange.Domain.Transfers;
using FluentValidation.TestHelper;
using Xunit;

namespace CurrencyExchange.Application.Tests.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandValidatorTests
{
    private readonly CreateTransferCommandValidator _validator = new();

    [Fact]
    public void Payer_ShouldHaveValationError_WhenNullOrEmpty()
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var recipient = Recipient.Create("Recipient Name", "Account Number", "Bank Code", "Bank Name");
        var command = new CreateTransferCommand(quoteId, null, recipient);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Payer)
            .WithErrorMessage("Payer is required");
    }

    [Fact]
    public void Recipient_ShouldHaveValationError_WhenNullOrEmpty()
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var payer = Payer.Create(Guid.NewGuid(), "Payer Name", "Transfer Reason");
        var command = new CreateTransferCommand(quoteId, payer, null);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Recipient)
            .WithErrorMessage("Recipient is required");
    }

    [Fact]
    public void ShouldNotHaveValidationErrors()
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var payer = Payer.Create(Guid.NewGuid(), "Payer Name", "Transfer Reason");
        var recipient = Recipient.Create("Recipient Name", "Account Number", "Bank Code", "Bank Name");
        var command = new CreateTransferCommand(quoteId, payer, recipient);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("", "test", "Payer name is required", "Payer.Name")]
    [InlineData(" ", "test", "Payer name is required", "Payer.Name")]
    [InlineData(null, "test", "Payer name is required", "Payer.Name")]
    [InlineData("test", "", "Payer transfer reason is required", "Payer.TransferReason")]
    [InlineData("test", "  ", "Payer transfer reason is required", "Payer.TransferReason")]
    [InlineData("test", null, "Payer transfer reason is required", "Payer.TransferReason")]
    public void Payer_ShouldHaveValidationError_WhenInvalid(string name, string transferReason,
        string expectedErrorMessage, string expectedProperty)
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var payer = Payer.Create(Guid.NewGuid(), name, transferReason);
        var recipient = Recipient.Create("Recipient Name", "Account Number", "Bank Code", "Bank Name");
        var command = new CreateTransferCommand(quoteId, payer, recipient);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(expectedProperty)
            .WithErrorMessage(expectedErrorMessage);
    }


    [Theory]
    [InlineData("", "123456789", "001", "Bank Name", "Recipient name is required", "Recipient.Name")]
    [InlineData(" ", "123456789", "001", "Bank Name", "Recipient name is required", "Recipient.Name")]
    [InlineData(null, "123456789", "001", "Bank Name", "Recipient name is required", "Recipient.Name")]
    [InlineData("Recipient Name", "", "001", "Bank Name", "Recipient account number is required",
        "Recipient.AccountNumber")]
    [InlineData("Recipient Name", " ", "001", "Bank Name", "Recipient account number is required",
        "Recipient.AccountNumber")]
    [InlineData("Recipient Name", null, "001", "Bank Name", "Recipient account number is required",
        "Recipient.AccountNumber")]
    [InlineData("Recipient Name", "123456789", "", "Bank Name", "Recipient bank code is required",
        "Recipient.BankCode")]
    [InlineData("Recipient Name", "123456789", " ", "Bank Name", "Recipient bank code is required",
        "Recipient.BankCode")]
    [InlineData("Recipient Name", "123456789", null, "Bank Name", "Recipient bank code is required",
        "Recipient.BankCode")]
    [InlineData("Recipient Name", "123456789", "001", "", "Recipient bank name is required", "Recipient.BankName")]
    [InlineData("Recipient Name", "123456789", "001", " ", "Recipient bank name is required", "Recipient.BankName")]
    [InlineData("Recipient Name", "123456789", "001", null, "Recipient bank name is required", "Recipient.BankName")]
    public void Recipient_ShouldHaveValidationError_WhenInvalid(
        string name,
        string accountNumber,
        string bankCode,
        string bankName,
        string expectedErrorMessage,
        string expectedProperty)
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var payer = Payer.Create(Guid.NewGuid(), "Payer Name", "Transfer Reason");
        var recipient = Recipient.Create(name, accountNumber, bankCode, bankName);
        var command = new CreateTransferCommand(quoteId, payer, recipient);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(expectedProperty)
            .WithErrorMessage(expectedErrorMessage);
    }


}