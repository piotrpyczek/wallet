using Wallet.Domain.Messaging;

namespace Wallet.Commands;

public class CreateWalletCommand : ICommand<Guid?>
{
    public string? Name { get; set; }
}