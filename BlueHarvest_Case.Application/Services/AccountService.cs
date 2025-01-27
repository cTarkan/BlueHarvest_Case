using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;


namespace BlueHarvest_Case.Application.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<Account> CreateAccountAsync(int customerId, decimal initialCredit)
		{
			var account = new Account(customerId, initialCredit);
			await _accountRepository.AddAsync(account);
			return account;
		}

		public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
		{
			return await _accountRepository.GetByCustomerIdAsync(customerId);
		}
	}
}
