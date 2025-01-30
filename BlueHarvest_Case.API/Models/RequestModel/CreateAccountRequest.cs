namespace BlueHarvest_Case.API.Models.RequestModel
{
	public class CreateAccountRequest
	{
		public int CustomerId { get; set; }
		public decimal InitialCredit { get; set; }
	}
}
