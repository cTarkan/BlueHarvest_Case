using BlueHarvest_Case.Domain.Entities;


namespace BlueHarvest_Case.Infrastructure.Interfaces
{
	public interface IAccountRepository
	{
		Task AddAsync(Account account);
		Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId);
	}
}
