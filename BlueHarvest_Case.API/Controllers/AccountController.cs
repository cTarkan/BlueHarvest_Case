using BlueHarvest_Case.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest_Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAccount([FromQuery] int customerId, [FromQuery] decimal initialCredit)
		{
			var account = await _accountService.CreateAccountAsync(customerId, initialCredit);
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
