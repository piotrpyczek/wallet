using System.Net.Http.Json;
using ExchangeRates.BackgroundTasks.Configuration;
using ExchangeRates.BackgroundTasks.Models;
using ExchangeRates.Domain;
using Microsoft.Extensions.Options;

namespace ExchangeRates.BackgroundTasks.Services;

public class RefreshCurrencyRatesService : BackgroundService
{
    private readonly ExchangeRatesSettings settings;
    private readonly ILogger<RefreshCurrencyRatesService> logger;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IExchangeRateService exchangeRateService;

    public RefreshCurrencyRatesService(IOptions<ExchangeRatesSettings> settings, ILogger<RefreshCurrencyRatesService> logger, IHttpClientFactory httpClientFactory, IExchangeRateService exchangeRateService)
    {
        this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this.exchangeRateService = exchangeRateService ?? throw new ArgumentNullException(nameof(exchangeRateService));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogDebug("RefreshCurrencyRatesService is starting.");

        stoppingToken.Register(() => logger.LogDebug("RefreshCurrencyRatesService background task is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogDebug("RefreshCurrencyRatesService running at: {time}", DateTime.Now);

            await SaveCurrentRates(stoppingToken);

            await Task.Delay(settings.RefreshPeriod, stoppingToken);
        }

        logger.LogDebug("RefreshCurrencyRatesService background task stoped.");
    }

    private async Task SaveCurrentRates(CancellationToken cancellationToken)
    {
        var rates = await GetRatesAsync(cancellationToken);

        var date = DateTime.Now;

        foreach (var rate in rates)
        {
            var exchangeRate = new ExchangeRate
            {
                Code = rate.Code,
                Currency = rate.Currency,
                Mid = rate.Mid,
                Date = date
            };

            await exchangeRateService.SaveExchageRateAsync(exchangeRate);
        }
    }

    private async Task<IEnumerable<Rate>> GetRatesAsync(CancellationToken cancellationToken)
    {
        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync(settings.Url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Error getting exchange rates");
            return new List<Rate>();
        }

        var tables = await response.Content.ReadFromJsonAsync<IEnumerable<Table>>(cancellationToken: cancellationToken);
        return tables.SelectMany(x => x.Rates);
    }
}