using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Exceptions;
using Wallet.Domain.Messaging;
using Wallet.Implementation;

namespace Wallet.Commands.Wallet.Handlers;

public class CreateWalletCommandHandler : CommandHandler<CreateWalletCommand, Guid?>
{
    private readonly WalletDbContext context;

    public CreateWalletCommandHandler(WalletDbContext context)
    {
        this.context = context;
    }

    public override async Task<Guid?> Handle(CreateWalletCommand command, CancellationToken cancellationToken)
    {
        if (await context.Wallets.AnyAsync(x => x.Name == command.Name, cancellationToken: cancellationToken))
        {
            throw new BadRequestException($"Wallet named '{command.Name}' already exists");
        }

        var wallet = new Domain.Entities.Wallet { Name = command.Name };
        await context.Wallets.AddAsync(wallet, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return wallet.Id;
    }
}