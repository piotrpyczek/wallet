namespace Wallet.API.Models;

public class WithdrawModel
{
    public string? CurrencyCode { get; set; }
    public decimal? Amount { get; set; }
}