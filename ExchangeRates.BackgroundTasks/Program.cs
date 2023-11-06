using ExchangeRates.BackgroundTasks.Infrastructure;
using ExchangeRates.BackgroundTasks.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationConfiguration(builder.Configuration);
builder.Services.AddHostedService<RefreshCurrencyRatesService>();

var app = builder.Build();

await app.RunAsync();