using FluentValidation;

namespace Wallet.Commands.Wallet.Validators;

public class WithdrawFromWalletCommandValidator : AbstractValidator<WithdrawFromWalletCommand>
{
    public WithdrawFromWalletCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.CurrencyCode).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
    }
}