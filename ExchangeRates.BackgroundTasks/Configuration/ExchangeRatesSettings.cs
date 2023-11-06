namespace ExchangeRates.BackgroundTasks.Configuration;

public class ExchangeRatesSettings
{
    public static string Section => "ExchangeRates";

    public string Url { get; set; }
    public TimeSpan RefreshPeriod { get; set; }
}