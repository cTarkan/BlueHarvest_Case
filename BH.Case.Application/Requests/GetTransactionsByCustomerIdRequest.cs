
using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Requests
{
	public class GetTransactionsByCustomerIdRequest : IRequest<IEnumerable<Transaction>>
	{
		public int CustomerId { get; set; }
	}
}
