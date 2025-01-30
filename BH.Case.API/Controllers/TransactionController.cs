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
	public class TransactionController : ControllerBase
	{
		private readonly IValidator<AddTransactionRequest> _validator;
		private readonly IMediator _mediator;
		public TransactionController(IValidator<AddTransactionRequest> validator, IMediator mediator)
		{
			_validator = validator;
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> AddTransaction([FromBody] AddTransactionRequest request)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			var transaction = await _mediator.Send(new AddTransactionCommand { AccountId = request.AccountId, Amount = request.Amount });
			return Ok(transaction);
		}

		[HttpGet]
		[Route("{customerId}")]
		public async Task<IActionResult> GetTransactionsByCustomerId(int customerId)
		{
			var transactions = await _mediator.Send(new GetTransactionsByCustomerIdRequest { CustomerId = customerId });
			return Ok(transactions);
		}
	}
}
