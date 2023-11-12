using Wallet.Domain.Messaging;

namespace Wallet.Commands.Wallet;

public class DepositConversionCommand : ICommand
{
    public DepositConversionCommand(Guid? walletId, string? currencyCodeFrom, string? currencyCodeTo, decimal? amount)
    {
        WalletId = walletId;
        CurrencyCodeFrom = currencyCodeFrom;
        CurrencyCodeTo = currencyCodeTo;
        Amount = amount;
    }

    public Guid? WalletId { get; }
    public string? CurrencyCodeFrom { get; }
    public string? CurrencyCodeTo { get; }
    public decimal? Amount { get; }
}