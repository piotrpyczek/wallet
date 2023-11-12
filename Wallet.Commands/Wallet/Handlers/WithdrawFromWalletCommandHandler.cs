using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Enums;
using Wallet.Domain.Messaging;
using Wallet.Implementation;

namespace Wallet.Commands.Wallet.Handlers;

public class WithdrawFromWalletCommandHandler : CommandHandler<WithdrawFromWalletCommand>
{
    private readonly WalletDbContext context;

    public WithdrawFromWalletCommandHandler(WalletDbContext context)
    {
        this.context = context;
    }

    public override async Task Handle(WithdrawFromWalletCommand command, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets
            .Include(x => x.Buckets)
            .ThenInclude(x => x.Transactions)
            .Where(x => x.Id == command.WalletId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (wallet == null)
        {
            throw new NotFoundException("Wallet not found");
        }

        var bucket = wallet.Buckets.FirstOrDefault(x => x.CurrencyCode == command.CurrencyCode);

        if (bucket == null)
        {
            throw new NotFoundException("Currency not found");
        }

        if (bucket.Amount < command.Amount)
        {
            throw new BadRequestException("Insufficient funds");
        }

        var transaction = CreateWithdrawTransaction(command.Amount ?? throw new BadRequestException("Amount not specified"));

        bucket.ApplyTransaction(transaction);

        context.Wallets.Update(wallet);
    }

    private Transaction CreateWithdrawTransaction(decimal amount)
    {
        var transaction = new Transaction
        {
            Amount = -Math.Abs(amount),
            Date = DateTime.Now,
            Status = TransactionStatus.Approved,
            Type = TransactionType.Withdrawal
        };

        return transaction;
    }
}