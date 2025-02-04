using BH.Case.Application.Commands;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateAccountCommandHandler(
			IAccountRepository accountRepository, 
			ITransactionRepository transactionRepository, 
			ICustomerRepository customerRepository,
			IUnitOfWork unitOfWork)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
			_customerRepository = customerRepository;
			_unitOfWork = unitOfWork;
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

			await _unitOfWork.BeginTransactionAsync(cancellationToken);
			try
			{
				var account = new Account(request.CustomerId, 0);
				await _accountRepository.AddAsync(account);
				await _unitOfWork.SaveChangesAsync(cancellationToken);

				if (request.InitialCredit > 0)
				{
					var transaction = new Transaction(account.Id, request.InitialCredit);
					await _transactionRepository.AddAsync(transaction);
					account.Balance += request.InitialCredit;
					await _unitOfWork.SaveChangesAsync(cancellationToken);
				}

				await _unitOfWork.CommitTransactionAsync(cancellationToken);
				return account;
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync(cancellationToken);
				throw;
			}
		}
	}
}
