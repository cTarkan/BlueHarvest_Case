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
		public async Task<IActionResult> CreateAccount([FromQuery] int customerId)
		{
			var account = await _accountService.CreateAccountAsync(customerId);
			return Ok(account);
		}
	}
}
