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

			var accountDetails = new List<AccountDetailsDto>();
			decimal totalBalance = 0;

			foreach (var account in accounts)
			{
				var transactions = await _transactionRepository.GetByAccountIdAsync(account.Id);
				decimal transactionTotal = transactions.Sum(t => t.Amount);

				accountDetails.Add(new AccountDetailsDto
				{
					AccountId = account.Id,
					Balance = transactionTotal,
					Transactions = transactions.Select(t => new TransactionDto
					{
						Amount = t.Amount,
						Timestamp = t.Timestamp
					}).ToList()
				});
				totalBalance += transactionTotal;
			}

			return new UserAccountDetailsDto
			{
				Name = user.Value.Name,
				Surname = user.Value.Surname,
				TotalBalance = totalBalance, 
				Accounts = accountDetails
			};
		}
	}
}
