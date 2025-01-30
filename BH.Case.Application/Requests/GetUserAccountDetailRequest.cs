
using BH.Case.Application.DTOs;
using MediatR;

namespace BH.Case.Application.Requests
{
	public class GetUserAccountDetailRequest : IRequest<UserAccountDetailsDto> 
	{
		public int CustomerId { get; set; }
	}
}
