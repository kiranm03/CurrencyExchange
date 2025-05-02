using CurrencyExchange.Application.Common.Interfaces;
using CurrencyExchange.Application.Quotes.Commands.CreateQuote;
using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using FluentAssertions;
using Moq;
using Xunit;

namespace CurrencyExchange.Application.Tests.Quotes.Commands.CreateQuote;

public class CreateQuoteCommandHandlerTests
{
    private readonly Mock<IExchangeRateService> _exchangeRateServiceMock = new();
    private readonly Mock<IQuoteRepository> _quoteRepositoryMock = new();
    private readonly CreateQuoteCommandHandler _handler;

    public CreateQuoteCommandHandlerTests()
    {
        _handler = new CreateQuoteCommandHandler(
            _exchangeRateServiceMock.Object,
            _quoteRepositoryMock.Object);
    }

    [Theory]
    [InlineData("INVALID", "USD", "SellCurrency")]
    [InlineData("AUD", "INVALID", "BuyCurrency")]
    public async Task Handle_ShouldReturnValidationError_ForInvalidCurrencyEnums(string sell, string buy, string expectedErrorCode)
    {
        // Arrange
        var command = new CreateQuoteCommand(sell, buy, 100);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(expectedErrorCode);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExchangeRateServiceFails()
    {
        // Arrange
        _exchangeRateServiceMock
            .Setup(x => x.GetExchangeRate(SellCurrency.AUD, BuyCurrency.USD, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Error.Failure("RateError", "Failed to fetch"));

        var command = new CreateQuoteCommand("AUD", "USD", 100);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("RateError");
    }

    [Fact]
    public async Task Handle_ShouldReturnQuote_WhenInputIsValid()
    {
        // Arrange
        var expectedRate = ExchangeRate.Create(0.75m);
        _exchangeRateServiceMock
            .Setup(x => x.GetExchangeRate(SellCurrency.AUD, BuyCurrency.USD, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRate);

        _quoteRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Quote>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new CreateQuoteCommand("AUD", "USD", 100);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.SellCurrency.Should().Be(SellCurrency.AUD);
        result.Value.BuyCurrency.Should().Be(BuyCurrency.USD);
    }
}
