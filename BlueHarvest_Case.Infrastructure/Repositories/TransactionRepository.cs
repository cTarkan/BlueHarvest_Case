using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;

namespace BlueHarvest_Case.Infrastructure.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly List<Transaction> _transactions = new();

		public Task AddAsync(Transaction transaction)
		{
			transaction.Id = _transactions.Count + 1;
			_transactions.Add(transaction);
			Console.WriteLine($"Transaction Added: ID={transaction.Id}, AccountID={transaction.AccountId}, Amount={transaction.Amount}");
			return Task.CompletedTask;
		}

		public Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
		{
			var transactions = _transactions.Where(t => t.AccountId == accountId);
			return Task.FromResult(transactions.AsEnumerable());
		}
	}
}

