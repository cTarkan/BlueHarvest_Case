using BlueHarvest_Case.Domain.Entities;

namespace BlueHarvest_Case.Application.Interfaces
{
	public interface IAccountService
	{
		Task<Account> CreateAccountAsync(int customerId, decimal initialCredit);
		Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);
	}
}
