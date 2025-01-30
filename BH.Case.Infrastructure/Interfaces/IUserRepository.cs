using BH.Case.Domain.Entities;

namespace BH.Case.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);
        Task AddAsync(User user);
    }
} 