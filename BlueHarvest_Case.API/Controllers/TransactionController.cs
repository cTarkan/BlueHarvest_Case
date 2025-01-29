using BlueHarvest_Case.API.DTOs;
using BlueHarvest_Case.Application.DTOs;
using BlueHarvest_Case.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest_Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;
		private readonly IValidator<AddTransactionRequest> _validator;
		public TransactionController(ITransactionService transactionService, IValidator<AddTransactionRequest> validator)
		{
			_transactionService = transactionService;
			_validator = validator;
		}

		[HttpPost]
		[Route("add")]
		public async Task<IActionResult> AddTransaction([FromBody] AddTransactionRequest request)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors);

			await _transactionService.AddTransactionAsync(request.AccountId, request.Amount);
			return Ok();
		}

		[HttpGet]
		[Route("{customerId}/transactions")]
		public async Task<IActionResult> GetTransactionsByCustomerId(int customerId)
		{
			var transactions = await _transactionService.GetTransactionsByCustomerIdAsync(customerId);
			return Ok(transactions);
		}
	}
}
