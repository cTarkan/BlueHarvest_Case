using BlueHarvest_Case.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest_Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserAccountService _userAccountService;

		public UserController(IUserAccountService userAccountService)
		{
			_userAccountService = userAccountService;
		}

		[HttpGet]
		[Route("{customerId}/details")]
		public async Task<IActionResult> GetUserAccountDetails(int customerId)
		{
			var details = await _userAccountService.GetUserAccountDetailsAsync(customerId);
			if (details == null) return NotFound("User not found.");

			return Ok(details);
		}
	}
}
