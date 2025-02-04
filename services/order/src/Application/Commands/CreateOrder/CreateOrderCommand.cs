using Domain.Models;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public CreateOrderDto OrderDto { get; }

        public CreateOrderCommand(CreateOrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}