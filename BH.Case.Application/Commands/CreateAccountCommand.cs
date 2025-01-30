using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Commands
{
	public class CreateAccountCommand : IRequest<Account>
	{
		public int CustomerId { get; set; }
		public decimal InitialCredit { get; set; }
	}
}
