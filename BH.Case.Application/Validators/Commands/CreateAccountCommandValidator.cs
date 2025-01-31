using BH.Case.Application.Commands;
using FluentValidation;

namespace BH.Case.Application.Validators.Commands
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0");

            RuleFor(x => x.InitialCredit)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Initial credit cannot be negative");
        }
    }
}