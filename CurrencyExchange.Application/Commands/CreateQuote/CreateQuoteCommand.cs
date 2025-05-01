using CurrencyExchange.Domain.Quotes;
using MediatR;

namespace CurrencyExchange.Application.Commands.CreateQuote;

public record CreateQuoteCommand(
    string SellCurrency,
    string BuyCurrency,
    decimal Amount) : IRequest<Quote>;