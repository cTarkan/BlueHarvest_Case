namespace BH.Case.Application.DTOs
{
	public class CustomerAccountDetailsDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public decimal TotalBalance { get; set; }
		public List<AccountDetailsDto> Accounts { get; set; }
	}
}
