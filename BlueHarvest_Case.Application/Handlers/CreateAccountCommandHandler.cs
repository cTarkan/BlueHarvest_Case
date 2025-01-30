using BlueHarvest_Case.Application.Commands;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using MediatR;

namespace BlueHarvest_Case.Application.Handlers
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IUserRepository _userRepository;

		public CreateAccountCommandHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IUserRepository userRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
		}

		public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
		{
			if (request.InitialCredit < 0)
			{
				throw new ArgumentException("Initial credit cannot be negative.");
			}

			var existingUser = await _userRepository.GetByIdAsync(request.CustomerId);
			if (existingUser == null)
			{
				throw new KeyNotFoundException("Customer does not exist.");
			}

			var account = new Account(request.CustomerId, 0);
			await _accountRepository.AddAsync(account);

			if (request.InitialCredit > 0)
			{
				var transaction = new Transaction(account.Id, request.InitialCredit);
				await _transactionRepository.AddAsync(transaction);
				account.Balance += request.InitialCredit;
			}

			return account;
		}
	}
}
