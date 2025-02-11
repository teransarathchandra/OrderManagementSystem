using Infrastructure.Persistence;
using MediatR;

namespace Application.Order.Update
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly OrderDbContext _dbContext;

        public UpdateOrderStatusCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(request.OrderId);

            if (order == null)
            {
                return false;
            }

            order.Status = request.Status;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}