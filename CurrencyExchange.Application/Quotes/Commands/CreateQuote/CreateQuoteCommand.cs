using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Commands.CreateQuote;

public record CreateQuoteCommand(string SellCurrency, string BuyCurrency, decimal Amount) : IRequest<ErrorOr<Quote>>;