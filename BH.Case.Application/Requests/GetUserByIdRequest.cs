using BH.Case.Domain.Entities;
using MediatR;

namespace BH.Case.Application.Requests
{
    public class GetUserByIdRequest : IRequest<User>
    {
        public int UserId { get; set; }
    }
} 