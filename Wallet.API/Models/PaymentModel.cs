namespace Wallet.API.Models;

public class PaymentModel
{
    public string? CurrencyCode { get; set; }
    public decimal? Amount { get; set; }
}