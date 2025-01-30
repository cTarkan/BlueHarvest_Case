	
using BH.Case.Application.Requests;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class GetTransactionsByCustomerIdRequestHandler : IRequestHandler<GetTransactionsByCustomerIdRequest, IEnumerable<Transaction>>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;

		public GetTransactionsByCustomerIdRequestHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
		}

		public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByCustomerIdRequest request, CancellationToken cancellationToken)
		{
			var accounts = await _accountRepository.GetByCustomerIdAsync(request.CustomerId);
			var transactions = new List<Transaction>();

			foreach (var account in accounts)
			{
				var accountTransactions = await _transactionRepository.GetByAccountIdAsync(account.Id);
				transactions.AddRange(accountTransactions);
			}

			return transactions;
		}
	}
}
    