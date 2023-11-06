namespace ExchangeRates.BackgroundTasks.Models;

public class Rate
{
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal Mid { get; set; }
}