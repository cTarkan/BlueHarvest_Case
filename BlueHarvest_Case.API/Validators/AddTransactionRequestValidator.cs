using BlueHarvest_Case.API.DTOs;
using FluentValidation;

namespace BlueHarvest_Case.API.Validators
{
	public class AddTransactionRequestValidator : AbstractValidator<AddTransactionRequest>
	{
		public AddTransactionRequestValidator()
		{
			RuleFor(x => x.AccountId)
				.GreaterThan(0).WithMessage("AccountId must be greater than 0");

			RuleFor(x => x.Amount)
				.GreaterThan(0).WithMessage("Transaction amount must be greater than 0");
		}
	}
}
