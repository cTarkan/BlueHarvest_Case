
using BH.Case.Application.DTOs;
using MediatR;

namespace BH.Case.Application.Requests
{
	public class GetCustomerAccountDetailRequest : IRequest<CustomerAccountDetailsDto> 
	{
		public int CustomerId { get; set; }
	}
}
