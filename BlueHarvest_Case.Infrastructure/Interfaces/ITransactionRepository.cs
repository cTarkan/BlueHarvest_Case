using BlueHarvest_Case.Domain.Entities;

namespace BlueHarvest_Case.Infrastructure.Interfaces
{
	public interface ITransactionRepository
	{
		Task AddAsync(Transaction transaction);
		Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
	}
}
