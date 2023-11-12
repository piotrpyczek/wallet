using Wallet.Domain.Messaging;

namespace Wallet.Commands.Wallet;

public class WithdrawFromWalletCommand : ICommand
{
    public WithdrawFromWalletCommand(Guid? walletId, string? currencyCode, decimal? amount)
    {
        WalletId = walletId;
        CurrencyCode = currencyCode;
        Amount = amount;
    }

    public Guid? WalletId { get; }
    public string? CurrencyCode { get; }
    public decimal? Amount { get; }
}