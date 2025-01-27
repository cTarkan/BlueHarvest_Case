using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;


namespace BlueHarvest_Case.Infrastructure.Repositories
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
	}
}
