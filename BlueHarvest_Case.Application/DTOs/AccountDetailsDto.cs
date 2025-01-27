namespace BlueHarvest_Case.Application.DTOs
{
	public class AccountDetailsDto
	{
		public int AccountId { get; set; }
		public decimal Balance { get; set; }
		public List<TransactionDto> Transactions { get; set; }
	}

}
