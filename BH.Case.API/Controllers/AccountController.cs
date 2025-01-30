using BH.Case.API.Models.RequestModel;
using BH.Case.Application.Commands;
using BH.Case.Application.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BH.Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IValidator<CreateAccountRequest> _validator;
		private readonly IMediator _mediator;

		public AccountController(IValidator<CreateAccountRequest> validator, IMediator mediator)
		{
			_validator = validator;
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			var account = await _mediator.Send(new CreateAccountCommand { CustomerId = request.CustomerId, InitialCredit = request.InitialCredit });
			return StatusCode(StatusCodes.Status201Created, account);
		}

		[HttpGet("{customerId}")]
		public async Task<IActionResult> GetCustomerAccount(int customerId)
		{
			var accounts = await _mediator.Send(new GetAccountsByCustomerIdRequest { CustomerId = customerId });
			return Ok(accounts);
		}
	}
}
