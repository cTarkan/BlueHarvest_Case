using BH.Case.Application.Commands;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Transaction>
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AddTransactionCommandHandler(
			ITransactionRepository transactionRepository, 
			IAccountRepository accountRepository,
			IUnitOfWork unitOfWork)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Transaction> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
		{
			var account = await _accountRepository.GetByIdAsync(request.AccountId);
	
			if (account == null)
			{
				throw new KeyNotFoundException($"Account with ID {request.AccountId} not found.");
			}

			await _unitOfWork.BeginTransactionAsync(cancellationToken);
			try
			{
				var transaction = new Transaction(request.AccountId, request.Amount);
				await _transactionRepository.AddAsync(transaction);
				
				account.Balance += request.Amount;
				
				await _unitOfWork.SaveChangesAsync(cancellationToken);
				await _unitOfWork.CommitTransactionAsync(cancellationToken);
				
				return transaction;
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync(cancellationToken);
				throw;
			}
		}
	}
}
