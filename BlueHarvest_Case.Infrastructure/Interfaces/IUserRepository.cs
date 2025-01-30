using BlueHarvest_Case.Domain.Entities;

namespace BlueHarvest_Case.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);
        Task AddAsync(User user);
    }
} 