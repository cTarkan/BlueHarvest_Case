using BlueHarvest_Case.Application.Commands;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using MediatR;

namespace BlueHarvest_Case.Application.Handlers
{
	public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Transaction>
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;

		public AddTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
		}

		public async Task<Transaction> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
		{
			var accounts = await _accountRepository.GetByCustomerIdAsync(request.AccountId);
			var account = accounts.FirstOrDefault();
			if (account == null)
			{
				throw new KeyNotFoundException($"Account with ID {request.AccountId} not found.");
			}

			var transaction = new Transaction(request.AccountId, request.Amount);
			await _transactionRepository.AddAsync(transaction);
			return transaction;
		}
	}
}
