using BH.Case.Application.Requests;
using FluentValidation;

namespace BH.Case.Application.Validators.Requests
{
    public class GetTransactionsByCustomerIdRequestValidator : AbstractValidator<GetTransactionsByCustomerIdRequest>
    {
        public GetTransactionsByCustomerIdRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0");
        }
    }
}