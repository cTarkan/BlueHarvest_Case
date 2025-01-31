using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;

namespace BH.Case.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public Task<Customer?> GetByIdAsync(int customerId)
        {
            var customer = _customers.FirstOrDefault(u => u.Id == customerId);
            return Task.FromResult(customer);
        }

        public Task AddAsync(Customer customer)
        {
            customer.Id = _customers.Count + 1;
            _customers.Add(customer);
            return Task.CompletedTask;
        }
    }
} 