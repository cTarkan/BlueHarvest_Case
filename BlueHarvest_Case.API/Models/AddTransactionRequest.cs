namespace BlueHarvest_Case.API.DTOs
{
	public class AddTransactionRequest
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
