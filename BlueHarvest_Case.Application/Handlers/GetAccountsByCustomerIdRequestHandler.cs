
using BlueHarvest_Case.Application.Requests;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using MediatR;

namespace BlueHarvest_Case.Application.Handlers
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