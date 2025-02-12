using Infrastructure.Persistence;
using MediatR;

namespace Application.Order.Update
{
    public class UpdateOrderStatusCommandHandler(OrderDbContext dbContext)
        : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders.FindAsync(request.OrderId);

            if (order == null)
            {
                return false;
            }

            order.Status = request.Status;
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
