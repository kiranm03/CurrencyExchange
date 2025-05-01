using CurrencyExchange.Domain.Quotes;
using MediatR;

namespace CurrencyExchange.Application.Queries.GetQuote;

public record GetQuoteQuery(Guid Id) : IRequest<Quote>;