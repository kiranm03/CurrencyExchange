using CurrencyExchange.Domain.Quotes;
using ErrorOr;
using MediatR;

namespace CurrencyExchange.Application.Quotes.Queries.GetQuote;

public record GetQuoteQuery(Guid Id) : IRequest<ErrorOr<Quote>>;