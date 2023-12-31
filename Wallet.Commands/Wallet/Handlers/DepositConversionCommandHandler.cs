﻿using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Enums;
using Wallet.Domain.Messaging;
using Wallet.Implementation;
using Wallet.Implementation.Services;

namespace Wallet.Commands.Wallet.Handlers;

public class DepositConversionCommandHandler : CommandHandler<DepositConversionCommand>
{
    private readonly WalletDbContext context;
    private readonly IExchangeRateService exchangeRateService;

    public DepositConversionCommandHandler(WalletDbContext context, IExchangeRateService exchangeRateService)
    {
        this.context = context;
        this.exchangeRateService = exchangeRateService;
    }

    public override async Task Handle(DepositConversionCommand command, CancellationToken cancellationToken)
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

        var bucketFrom = wallet.Buckets.FirstOrDefault(x => x.CurrencyCode == command.CurrencyCodeFrom);
        if (bucketFrom == null)
        {
            throw new NotFoundException("Currency not found");
        }

        if (bucketFrom.Amount < command.Amount)
        {
            throw new BadRequestException("Insufficient funds");
        }

        var exchangeRateFrom = await exchangeRateService.GetExchangeRateAsync(command.CurrencyCodeFrom, cancellationToken);
        var currencyFromValue = exchangeRateFrom.ExchangeRate * command.Amount;
        var shiftFromTransaction = CreateShiftTransaction(-Math.Round(command.Amount.Value, 2, MidpointRounding.AwayFromZero));
        bucketFrom.ApplyTransaction(shiftFromTransaction);

        var exchangeRateTo = await exchangeRateService.GetExchangeRateAsync(command.CurrencyCodeTo, cancellationToken);
        var currencyToValue = currencyFromValue / exchangeRateTo.ExchangeRate;
        var shiftToTransaction = CreateShiftTransaction(Math.Round(currencyToValue.Value, 2, MidpointRounding.AwayFromZero));
        shiftToTransaction.ReferencedTransaction = shiftFromTransaction;

        var bucketTo = wallet.Buckets.FirstOrDefault(x => x.CurrencyCode == command.CurrencyCodeTo);
        if (bucketTo == null)
        {
            bucketTo = new CurrencyBucket(command.CurrencyCodeTo, exchangeRateTo.Currency);
            wallet.Buckets.Add(bucketTo);
        }

        bucketTo.ApplyTransaction(shiftToTransaction);

        context.Wallets.Update(wallet);
    }

    private Transaction CreateShiftTransaction(decimal amount)
    {
        var transaction = new Transaction
        {
            Amount = amount,
            Date = DateTime.Now,
            Status = TransactionStatus.Approved,
            Type = TransactionType.Shift
        };

        return transaction;
    }
}