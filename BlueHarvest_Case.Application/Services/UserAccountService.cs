using BlueHarvest_Case.Application.DTOs;
using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Infrastructure.Interfaces;

namespace BlueHarvest_Case.Application.Services
{
	public class UserAccountService : IUserAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;

		public UserAccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
		}

		public async Task<UserAccountDetailsDto> GetUserAccountDetailsAsync(int customerId)
		{
			var user = UserMockService.GetUser(customerId);
			if (user == null) return null;

			var accounts = await _accountRepository.GetByCustomerIdAsync(customerId);
			decimal totalBalance = accounts.Sum(a => a.Balance);

			var accountDetails = new List<AccountDetailsDto>();

			foreach (var account in accounts)
			{
				var transactions = await _transactionRepository.GetByAccountIdAsync(account.Id);
				accountDetails.Add(new AccountDetailsDto
				{
					AccountId = account.Id,
					Balance = transactions.Sum(x=>x.Amount),
					Transactions = transactions.Select(t => new TransactionDto
					{
						Amount = t.Amount,
						Timestamp = t.Timestamp
					}).ToList()
				});
			}

			return new UserAccountDetailsDto
			{
				Name = user.Value.Name,
				Surname = user.Value.Surname,
				TotalBalance = accountDetails.Sum(x=>x.Balance),
				Accounts = accountDetails
			};
		}
	}
}
