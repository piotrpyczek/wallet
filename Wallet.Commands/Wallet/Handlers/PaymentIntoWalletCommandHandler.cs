using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Enums;
using Wallet.Domain.Messaging;
using Wallet.Implementation;
using Wallet.Implementation.Services;

namespace Wallet.Commands.Wallet.Handlers;

public class PaymentIntoWalletCommandHandler : CommandHandler<PaymentIntoWalletCommand>
{
    private readonly WalletDbContext context;
    private readonly IExchangeRateService exchangeRateService;

    public PaymentIntoWalletCommandHandler(WalletDbContext context, IExchangeRateService exchangeRateService)
    {
        this.context = context;
        this.exchangeRateService = exchangeRateService;
    }

    public override async Task Handle(PaymentIntoWalletCommand command, CancellationToken cancellationToken)
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
            var avialableCurrencies = await exchangeRateService.GetCurrenciesAsync(cancellationToken);
            var currency = avialableCurrencies.FirstOrDefault(x => x.Code == command.CurrencyCode);

            if (currency == null)
            {
                throw new NotFoundException("Currency not found");
            }

            bucket = new CurrencyBucket(command.CurrencyCode, currency.Currency);
            wallet.Buckets.Add(bucket);
        }

        var transaction = CreatePaymentTransaction(command.Amount ?? throw new BadRequestException("Amount not specified"));

        bucket.ApplyTransaction(transaction);

        context.Wallets.Update(wallet);
    }

    private Transaction CreatePaymentTransaction(decimal amount)
    {
        var transaction = new Transaction
        {
            Amount = Math.Abs(amount),
            Date = DateTime.Now,
            Status = TransactionStatus.Approved,
            Type = TransactionType.Payment
        };

        return transaction;
    }
}