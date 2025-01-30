
using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Requests
{
	public class GetAccountsByCustomerIdRequest : IRequest<IEnumerable<Account>>
	{
		public int CustomerId { get; set; }
	}
}
