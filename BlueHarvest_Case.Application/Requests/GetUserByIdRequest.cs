using BlueHarvest_Case.Domain.Entities;
using MediatR;

namespace BlueHarvest_Case.Application.Requests
{
    public class GetUserByIdRequest : IRequest<User>
    {
        public int UserId { get; set; }
    }
} 