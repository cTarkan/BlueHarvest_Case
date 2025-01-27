using BlueHarvest_Case.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest_Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpPost]
		[Route("add")]
		public async Task<IActionResult> AddTransaction([FromQuery] int accountId, [FromQuery] decimal amount)
		{
			await _transactionService.AddTransactionAsync(accountId, amount);
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
