using BH.Case.Application.Commands;
using FluentValidation;

namespace BH.Case.Application.Validators.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("Name can only contain letters and spaces");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .MaximumLength(100)
                .WithMessage("Surname cannot exceed 100 characters")
                .Matches(@"^[a-zA-Z\s]*$")
                .WithMessage("Surname can only contain letters and spaces");
        }
    }
}