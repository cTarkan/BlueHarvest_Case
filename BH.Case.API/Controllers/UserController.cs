using BH.Case.API.Models.RequestModel;
using BH.Case.Application.Commands;
using BH.Case.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BH.Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("{customerId}/details")]
		public async Task<IActionResult> GetUserAccountDetails(int customerId)
		{
			var details = await _mediator.Send(new GetUserAccountDetailRequest { CustomerId = customerId });
			if (details == null) return NotFound("User not found.");
		
			return Ok(details);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
		{
			var user = await _mediator.Send(new CreateUserCommand { Name = request.Name, Surname = request.Surname });
			return Ok(user);
		}

		[HttpGet]
		[Route("{customerId}")]
		public async Task<IActionResult> GetUserById(int customerId)
		{
			var user = await _mediator.Send(new GetUserByIdRequest { UserId = customerId });
			return Ok(user);
		}
	}
}
