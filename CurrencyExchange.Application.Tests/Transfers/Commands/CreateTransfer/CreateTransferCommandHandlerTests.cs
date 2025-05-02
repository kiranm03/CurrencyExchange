using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Application.Transfers.Commands.CreateTransfer;
using CurrencyExchange.Domain.Quotes;
using CurrencyExchange.Domain.Transfers;
using FluentAssertions;
using Moq;
using Xunit;

namespace CurrencyExchange.Application.Tests.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandHandlerTests
{
    private readonly Mock<IQuoteRepository> _quoteRepositoryMock = new();
    private readonly Mock<ITransferRepository> _transferRepositoryMock = new();
    private readonly CreateTransferCommandHandler _handler;

    public CreateTransferCommandHandlerTests()
    {
        _handler = new CreateTransferCommandHandler(
            _quoteRepositoryMock.Object,
            _transferRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenQuoteNotFound()
    {
        // Arrange
        _quoteRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Quote?)null);

        var command = new CreateTransferCommand(Guid.NewGuid(),
            Payer.Create(Guid.NewGuid(), "John", "Invoice"),
            Recipient.Create("Alice", "123", "XYZ", "Test Bank"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("QuoteId");
        result.FirstError.Description.Should().Be("Quote not found.");
    }

    [Fact]
    public async Task Handle_ShouldCreateTransfer_WhenQuoteExists()
    {
        // Arrange
        var quoteId = Guid.NewGuid();
        var payer = Payer.Create(Guid.NewGuid(), "John", "Invoice");
        var recipient = Recipient.Create("Alice", "123", "XYZ", "Test Bank");
        _quoteRepositoryMock
            .Setup(repo => repo.GetByIdAsync(quoteId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Quote.Create(SellCurrency.AUD, BuyCurrency.USD, 100, ExchangeRate.Create(0.75m).Value));

        _transferRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Transfer>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new CreateTransferCommand(quoteId, payer, recipient);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Payer.Should().BeEquivalentTo(payer);
        result.Value.Recipient.Should().BeEquivalentTo(recipient);
    }
}
