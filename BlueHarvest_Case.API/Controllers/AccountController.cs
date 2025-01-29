using BlueHarvest_Case.API.DTOs;
using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest_Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IValidator<CreateAccountRequest> _validator;

		public AccountController(IAccountService accountService, IValidator<CreateAccountRequest> validator)
		{
			_accountService = accountService;
			_validator = validator;
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			var account = await _accountService.CreateAccountAsync(request.CustomerId, request.InitialCredit);
			return Ok(account);
		}

		[HttpGet]
		[Route("{customerId}/accounts")]
		public async Task<IActionResult> GetAccountsByCustomerId(int customerId)
		{
			var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
			return Ok(accounts);
		}
	}
}
