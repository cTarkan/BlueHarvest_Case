namespace BH.Case.API.Models.RequestModel
{
	public class AddTransactionRequest
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
