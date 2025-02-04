using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BH.Case.Infrastructure.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ApplicationDbContext _context;

		public AccountRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Account account)
		{
			await _context.Accounts.AddAsync(account);
		}

		public async Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId)
		{
			return await _context.Accounts
				.Where(a => a.CustomerId == customerId)
				.ToListAsync();
		}

		public async Task<Account?> GetByIdAsync(int accountId)
		{
			return await _context.Accounts.FindAsync(accountId);
		}
	}
}
