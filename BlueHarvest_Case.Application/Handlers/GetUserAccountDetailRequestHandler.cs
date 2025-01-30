using BlueHarvest_Case.Application.DTOs;
using BlueHarvest_Case.Application.Requests;
using BlueHarvest_Case.Infrastructure.Interfaces;
using MediatR;

namespace BlueHarvest_Case.Application.Handlers
{
	public class GetUserAccountDetailRequestHandler : IRequestHandler<GetUserAccountDetailRequest, UserAccountDetailsDto>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IUserRepository _userRepository;

		public GetUserAccountDetailRequestHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IUserRepository userRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
		}

		public async Task<UserAccountDetailsDto> Handle(GetUserAccountDetailRequest request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetByIdAsync(request.CustomerId);
			if (user == null) 
            {
                throw new KeyNotFoundException($"User with ID {request.CustomerId} not found.");
            }

			var accounts = await _accountRepository.GetByCustomerIdAsync(request.CustomerId);

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
				Name = user.Name,
				Surname = user.Surname,
				TotalBalance = totalBalance,
				Accounts = accountDetails
			};
		}

	}
}
    