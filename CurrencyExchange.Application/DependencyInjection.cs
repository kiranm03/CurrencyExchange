using CurrencyExchange.Application.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        
        return services;
    }
}