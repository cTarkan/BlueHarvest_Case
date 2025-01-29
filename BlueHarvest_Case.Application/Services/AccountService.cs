using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;


namespace BlueHarvest_Case.Application.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionService _transactionService;

		public AccountService(IAccountRepository accountRepository, ITransactionService transactionService)
		{
			_accountRepository = accountRepository;
			_transactionService = transactionService;
		}

		public async Task<Account> CreateAccountAsync(int customerId, decimal initialCredit)
		{
			if (initialCredit < 0)
			{
				throw new ArgumentException("Initial credit cannot be negative.");
			}

			var account = new Account(customerId, 0);
			await _accountRepository.AddAsync(account);

			if (initialCredit > 0)
			{
				await _transactionService.AddTransactionAsync(account.Id, initialCredit);
				account.Balance += initialCredit;
			}

			return account;
		}

		public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
		{
			return await _accountRepository.GetByCustomerIdAsync(customerId);
		}
	}
}
