using BH.Case.Domain.Entities;

namespace BH.Case.Infrastructure.Interfaces
{
	public interface ITransactionRepository
	{
		Task AddAsync(Transaction transaction);
		Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
	}
}
