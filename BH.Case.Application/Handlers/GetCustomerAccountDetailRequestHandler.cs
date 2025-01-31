using BH.Case.Application.DTOs;
using BH.Case.Application.Requests;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class GetCustomerAccountDetailRequestHandler : IRequestHandler<GetCustomerAccountDetailRequest, CustomerAccountDetailsDto>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly ICustomerRepository _customerRepository;

		public GetCustomerAccountDetailRequestHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository)
		{
			_accountRepository = accountRepository;
			_transactionRepository = transactionRepository;
			_customerRepository = customerRepository;
		}

		public async Task<CustomerAccountDetailsDto> Handle(GetCustomerAccountDetailRequest request, CancellationToken cancellationToken)
		{
			var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
			if (customer == null) 
            {
                throw new KeyNotFoundException($"Customer with ID {request.CustomerId} not found.");
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

			return new CustomerAccountDetailsDto
			{
				Name = customer.Name,
				Surname = customer.Surname,
				TotalBalance = totalBalance,
				Accounts = accountDetails
			};
		}

	}
}
    