
using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Requests
{
	public class GetTransactionsByCustomerIdRequest : IRequest<IEnumerable<Transaction>>
	{
		public int CustomerId { get; set; }
	}
}
