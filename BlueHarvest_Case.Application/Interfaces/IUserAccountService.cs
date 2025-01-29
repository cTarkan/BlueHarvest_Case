using BlueHarvest_Case.Application.DTOs;

namespace BlueHarvest_Case.Application.Interfaces
{
	public interface IUserAccountService
	{
		Task<UserAccountDetailsDto> GetUserAccountDetailsAsync(int customerId);
	}
}
