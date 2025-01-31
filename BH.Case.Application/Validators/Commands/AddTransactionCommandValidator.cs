using BH.Case.Application.Commands;
using FluentValidation;

namespace BH.Case.Application.Validators.Commands
{
    public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
    {
        public AddTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage("Account ID must be greater than 0");

            RuleFor(x => x.Amount)
                .NotEqual(0)
                .WithMessage("Transaction amount cannot be zero")
                .GreaterThan(0)
                .WithMessage("Transaction amount must be greater than 0");
        }
    }
}