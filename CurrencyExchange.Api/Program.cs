using CurrencyExchange.Api;
using CurrencyExchange.Application;
using CurrencyExchange.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();