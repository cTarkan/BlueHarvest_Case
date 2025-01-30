using BH.Case.Domain.Entities;


namespace BH.Case.Infrastructure.Interfaces
{
	public interface IAccountRepository
	{
		Task AddAsync(Account account);
		Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId);
	}
}
