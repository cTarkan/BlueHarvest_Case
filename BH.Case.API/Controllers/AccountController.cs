using BH.Case.API.Models.RequestModel;
using BH.Case.Application.Commands;
using BH.Case.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BH.Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
		{
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
