namespace ExchangeRates.Implementation.Configuration;

public class ExchangeRatesDatabaseSettings
{
    public static string Section => "ExchangeRatesDatabase";

    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}