using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Order.Delete
{
    public class CancelOrderCommandHandler(OrderDbContext dbContext) : IRequestHandler<CancelOrderCommand, bool>
    {
        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders.FindAsync(request.OrderId);

            if (order == null)
            {
                return false;
            }

            order.Status = OrderStatus.Canceled;
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
