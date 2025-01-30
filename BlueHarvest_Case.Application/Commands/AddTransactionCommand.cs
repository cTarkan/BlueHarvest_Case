using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Commands
{
	public class AddTransactionCommand : IRequest<Transaction>
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
