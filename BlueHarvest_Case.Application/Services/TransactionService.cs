using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;


namespace BlueHarvest_Case.Application.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;

		public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
		}

		public async Task AddTransactionAsync(int accountId, decimal amount)
		{
			var transaction = new Transaction(accountId, amount);
			await _transactionRepository.AddAsync(transaction);
		}

		public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerIdAsync(int customerId)
		{
			var accounts = await _accountRepository.GetByCustomerIdAsync(customerId);
			var transactions = new List<Transaction>();

			foreach (var account in accounts)
			{
				var accountTransactions = await _transactionRepository.GetByAccountIdAsync(account.Id);
				transactions.AddRange(accountTransactions);
			}

			return transactions;
		}
	}
}
