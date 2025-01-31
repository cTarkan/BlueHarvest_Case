using BH.Case.Domain.Entities;

namespace BH.Case.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int customerId);
        Task AddAsync(Customer customer);
    }
} 