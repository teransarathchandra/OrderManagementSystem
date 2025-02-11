using Domain.Models;
using MediatR;

namespace Application.Order.Create
{
    public class CreateOrderCommand : IRequest<Domain.Models.Order>
    {
        public CreateOrderDto OrderDto { get; }

        public CreateOrderCommand(CreateOrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
