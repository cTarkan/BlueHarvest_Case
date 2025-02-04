using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;


namespace BH.Case.Infrastructure.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly List<Account> _accounts = new();

		public Task AddAsync(Account account)
		{
			account.Id = _accounts.Count + 1;
			_accounts.Add(account);
			return Task.CompletedTask;
		}

		public Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId)
		{
			var accounts = _accounts.Where(a => a.CustomerId == customerId);
			return Task.FromResult(accounts.AsEnumerable());
		}

		public Task<Account?> GetByIdAsync(int accountId)
		{
			var account = _accounts.FirstOrDefault(a => a.Id == accountId);
			return Task.FromResult(account);
		}
	}
}
