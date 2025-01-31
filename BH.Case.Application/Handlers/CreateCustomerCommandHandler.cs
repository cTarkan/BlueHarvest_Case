using BH.Case.Application.Commands;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _cutomerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository cutomerRepository)
        {
            _cutomerRepository = cutomerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(request.Name));
            }

            if (string.IsNullOrEmpty(request.Surname))
            {
                throw new ArgumentException("Surname cannot be null or empty.", nameof(request.Surname));
            }

            var cutomer = new Customer
            {
                Name = request.Name,
                Surname = request.Surname
            };

            await _cutomerRepository.AddAsync(cutomer);
            return cutomer;
        }
    }
}