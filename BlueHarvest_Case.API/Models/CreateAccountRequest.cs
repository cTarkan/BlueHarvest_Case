namespace BlueHarvest_Case.API.DTOs
{
	public class CreateAccountRequest
	{
		public int CustomerId { get; set; }
		public decimal InitialCredit { get; set; }
	}
}
