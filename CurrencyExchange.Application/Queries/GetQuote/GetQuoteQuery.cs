using CurrencyExchange.Domain.Quotes;
using MediatR;
using ErrorOr;

namespace CurrencyExchange.Application.Queries.GetQuote;

public record GetQuoteQuery(Guid Id) : IRequest<ErrorOr<Quote>>;