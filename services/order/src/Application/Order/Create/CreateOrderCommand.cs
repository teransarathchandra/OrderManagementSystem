using Domain.Models;
using MediatR;

namespace Application.Order.Create
{
    public class CreateOrderCommand(CreateOrderDto orderDto) : IRequest<Domain.Models.Order>
    {
        public CreateOrderDto OrderDto { get; } = orderDto;
    }
}
