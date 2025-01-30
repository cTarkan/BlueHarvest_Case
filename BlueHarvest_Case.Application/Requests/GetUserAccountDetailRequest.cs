
using BlueHarvest_Case.Application.DTOs;
using MediatR;

namespace BlueHarvest_Case.Application.Requests
{
	public class GetUserAccountDetailRequest : IRequest<UserAccountDetailsDto> 
	{
		public int CustomerId { get; set; }
	}
}
