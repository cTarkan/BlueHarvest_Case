using BlueHarvest_Case.API.Models.RequestModel;
using FluentValidation;

namespace BlueHarvest_Case.Application.Validators
{
	public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
	{
		public CreateAccountRequestValidator()
		{
			RuleFor(x => x.CustomerId)
				.GreaterThan(0).WithMessage("CustomerId must be greater than 0");

			RuleFor(x => x.InitialCredit)
				.GreaterThanOrEqualTo(0).WithMessage("Initial credit cannot be negative");
		}
	}
}
