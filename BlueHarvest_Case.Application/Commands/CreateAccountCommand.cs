using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Commands
{
	public class CreateAccountCommand : IRequest<Account>
	{
		public int CustomerId { get; set; }
		public decimal InitialCredit { get; set; }
	}
}
