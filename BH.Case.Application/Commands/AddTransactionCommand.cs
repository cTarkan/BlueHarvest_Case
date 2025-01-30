using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Commands
{
	public class AddTransactionCommand : IRequest<Transaction>
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
