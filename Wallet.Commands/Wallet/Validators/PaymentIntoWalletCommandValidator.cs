using FluentValidation;

namespace Wallet.Commands.Wallet.Validators;

public class PaymentIntoWalletCommandValidator : AbstractValidator<PaymentIntoWalletCommand>
{
    public PaymentIntoWalletCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.CurrencyCode).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
    }
}