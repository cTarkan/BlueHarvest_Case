using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BH.Case.Infrastructure.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly ApplicationDbContext _context;

		public TransactionRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Transaction transaction)
		{
			await _context.Transactions.AddAsync(transaction);
		}

		public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
		{
			return await _context.Transactions
				.Where(t => t.AccountId == accountId)
				.OrderByDescending(t => t.Timestamp)
				.ToListAsync();
		}
	}
}

