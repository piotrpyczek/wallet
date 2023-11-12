using Wallet.Domain.Messaging;

namespace Wallet.Commands.Wallet;

public class PaymentIntoWalletCommand : ICommand
{
    public PaymentIntoWalletCommand(Guid? walletId, string? currencyCode, decimal? amount)
    {
        WalletId = walletId;
        CurrencyCode = currencyCode;
        Amount = amount;
    }

    public Guid? WalletId { get; }
    public string? CurrencyCode { get; }
    public decimal? Amount { get; }
}