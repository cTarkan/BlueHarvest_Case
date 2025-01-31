using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
} 