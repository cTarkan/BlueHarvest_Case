using BH.Case.Application.Requests;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
    public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            return customer;
        }
    }
} 