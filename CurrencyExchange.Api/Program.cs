using CurrencyExchange.Api;
using CurrencyExchange.Application;
using CurrencyExchange.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services before building the app
builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();