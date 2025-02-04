using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Case.Domain.Entities
{
	public class Account
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public decimal Balance { get; set; }

		// Parameterless constructor for EF Core
		protected Account() { }

		public Account(int customerId, decimal initialCredit)
		{
			CustomerId = customerId;
			Balance = initialCredit;
		}
	}
}
