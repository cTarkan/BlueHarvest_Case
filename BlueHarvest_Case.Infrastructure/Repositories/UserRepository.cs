using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;

namespace BlueHarvest_Case.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<User?> GetByIdAsync(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return Task.FromResult(user);
        }

        public Task AddAsync(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
} 