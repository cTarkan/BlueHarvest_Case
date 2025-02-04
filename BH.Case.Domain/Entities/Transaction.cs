namespace BH.Case.Domain.Entities
{
	public class Transaction
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Timestamp { get; set; }

		// Parameterless constructor for EF Core
		protected Transaction() { }

		public Transaction(int accountId, decimal amount)
		{
			AccountId = accountId;
			Amount = amount;
			Timestamp = DateTime.UtcNow;
		}
	}
}
