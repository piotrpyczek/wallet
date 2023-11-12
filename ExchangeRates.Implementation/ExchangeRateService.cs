using Common.Exceptions;
using ExchangeRates.Domain;
using ExchangeRates.Implementation.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExchangeRates.Implementation;

public class ExchangeRateService : IExchangeRateService
{
    private readonly ExchangeRatesDatabaseSettings databaseSettings;
    private readonly ILogger<ExchangeRateService> logger;
    private readonly IMongoCollection<ExchangeRate> exchangeRates;

    public ExchangeRateService(IOptions<ExchangeRatesDatabaseSettings> databaseSettings, ILogger<ExchangeRateService> logger)
    {
        this.databaseSettings = databaseSettings?.Value ?? throw new ArgumentNullException(nameof(databaseSettings));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var client = new MongoClient(this.databaseSettings.ConnectionString);
        var database = client.GetDatabase(this.databaseSettings.DatabaseName);
        exchangeRates = database.GetCollection<ExchangeRate>(this.databaseSettings.CollectionName);
    }

    public async Task SaveExchageRateAsync(ExchangeRate exchangeRate)
    {
        await exchangeRates.InsertOneAsync(exchangeRate);
    }

    public async Task<ExchangeRate> GetCurrentExchageRateAsync(string code)
    {
        var currentExchangeRate = await exchangeRates.AsQueryable()
            .Where(x => x.Code == code)
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();

        if (currentExchangeRate == null)
        {
            logger.LogInformation("Currency {code} exchage rate not found", code);
            throw new NotFoundException($"Currency {code} exchage rate not found");
        }

        return currentExchangeRate;
    }

}