using BlueHarvest_Case.Application.Commands;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using MediatR;

namespace BlueHarvest_Case.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(request.Name));
            }

            if (string.IsNullOrEmpty(request.Surname))
            {
                throw new ArgumentException("Surname cannot be null or empty.", nameof(request.Surname));
            }

            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname
            };

            await _userRepository.AddAsync(user);
            return user;
        }
    }
}