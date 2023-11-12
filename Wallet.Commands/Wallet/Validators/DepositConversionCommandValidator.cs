using FluentValidation;

namespace Wallet.Commands.Wallet.Validators;

public class DepositConversionCommandValidator : AbstractValidator<DepositConversionCommand>
{
    public DepositConversionCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.CurrencyCodeFrom).NotEmpty();
        RuleFor(x => x.CurrencyCodeTo).NotEmpty();
        RuleFor(x => x.Amount).NotNull().GreaterThan(0);
    }
}