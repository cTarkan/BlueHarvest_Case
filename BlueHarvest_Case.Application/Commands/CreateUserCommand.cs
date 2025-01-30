using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
} 