using BH.Case.Application.Requests;
using FluentValidation;

namespace BH.Case.Application.Validators.Requests
{
    public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
    {
        public GetUserByIdRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0");
        }
    }
} 