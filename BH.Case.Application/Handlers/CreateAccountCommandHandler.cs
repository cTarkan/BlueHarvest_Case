using BH.Case.Application.Commands;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using BH.Case.Infrastructure.Repositories;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly ICustomerRepository _customerRepository;

		public CreateAccountCommandHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
			_customerRepository = customerRepository;
		}

		public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
		{
			if (request.InitialCredit < 0)
			{
				throw new ArgumentException("Initial credit cannot be negative.");
			}

			var existingCustomer = await _customerRepository.GetByIdAsync(request.CustomerId);
			if (existingCustomer == null)
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
