using BlueHarvest_Case.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHarvest_Case.Application.Interfaces
{
	public interface ITransactionService
	{
		Task AddTransactionAsync(int accountId, decimal amount);
		Task<IEnumerable<Transaction>> GetTransactionsByCustomerIdAsync(int customerId);
	}
}
