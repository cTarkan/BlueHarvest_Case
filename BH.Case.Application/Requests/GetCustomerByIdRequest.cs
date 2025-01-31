using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Requests
{
    public class GetCustomerByIdRequest : IRequest<Customer>
    {
        public int CustomerId { get; set; }
    }
} 