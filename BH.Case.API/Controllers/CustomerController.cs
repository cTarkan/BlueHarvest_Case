using BH.Case.API.Models.RequestModel;
using BH.Case.Application.Commands;
using BH.Case.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BH.Case.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CustomerController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("{customerId}/details")]
		public async Task<IActionResult> GetCustomerAccountDetails(int customerId)
		{
			var details = await _mediator.Send(new GetCustomerAccountDetailRequest { CustomerId = customerId });
			if (details == null) return NotFound("Customer not found.");
		
			return Ok(details);
		}
			
		[HttpPost]
		public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
		{
			var customer = await _mediator.Send(new CreateCustomerCommand { Name = request.Name, Surname = request.Surname });
			return Ok(customer);
		}

		[HttpGet]
		[Route("{customerId}")]
		public async Task<IActionResult> GetCustomerById(int customerId)
		{
			var customer = await _mediator.Send(new GetCustomerByIdRequest { CustomerId = customerId });
			return Ok(customer);
		}
	}
}
