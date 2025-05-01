using CurrencyExchange.Domain.Quotes;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Commands.CreateQuote;

public record CreateQuoteCommand(
    string SellCurrency,
    string BuyCurrency,
    decimal Amount) : IRequest<ErrorOr<Quote>>;