
using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Requests
{
	public class GetAccountsByCustomerIdRequest : IRequest<IEnumerable<Account>>
	{
		public int CustomerId { get; set; }
	}
}
