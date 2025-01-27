namespace BlueHarvest_Case.Domain.Entities
{
	public class Transaction
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Timestamp { get; set; }

		public Transaction(int accountId, decimal amount)
		{
			AccountId = accountId;
			Amount = amount;
			Timestamp = DateTime.UtcNow;
		}
	}
}
