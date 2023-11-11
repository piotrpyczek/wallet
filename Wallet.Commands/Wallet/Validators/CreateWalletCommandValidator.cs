using FluentValidation;

namespace Wallet.Commands.Wallet.Validators
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
