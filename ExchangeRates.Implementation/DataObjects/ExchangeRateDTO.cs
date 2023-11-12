namespace ExchangeRates.Implementation.DataObjects;

public class ExchangeRateDTO
{
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal ExchangeRate { get; set; }
}