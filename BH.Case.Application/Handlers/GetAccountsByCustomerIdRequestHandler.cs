
using BH.Case.Application.Requests;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
	public class GetAccountsByCustomerIdRequestHandler : IRequestHandler<GetAccountsByCustomerIdRequest, IEnumerable<Account>>
	{
		private readonly IAccountRepository _accountRepository;

		public GetAccountsByCustomerIdRequestHandler(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<IEnumerable<Account>> Handle(GetAccountsByCustomerIdRequest request, CancellationToken cancellationToken)
		{
			return await _accountRepository.GetByCustomerIdAsync(request.CustomerId);
		}

	}
}