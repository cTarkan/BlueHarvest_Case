using BH.Case.Application.Commands;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using MediatR;

namespace BH.Case.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
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

            var customer = new Customer(request.Name, request.Surname);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return customer;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}