namespace Wallet.API.Models;

public class DepositConversionModel
{
    public string? CurrencyCodeFrom { get; set; }
    public string? CurrencyCodeTo { get; set; }
    public decimal? Amount { get; set; }
}