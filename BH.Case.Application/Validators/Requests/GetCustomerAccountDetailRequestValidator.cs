using BH.Case.Application.Requests;
using FluentValidation;

namespace BH.Case.Application.Validators.Requests
{
    public class GetCustomerAccountDetailRequestValidator : AbstractValidator<GetCustomerAccountDetailRequest>
    {
        public GetCustomerAccountDetailRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0");
        }
    }
}