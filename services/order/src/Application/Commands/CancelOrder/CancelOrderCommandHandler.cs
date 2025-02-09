using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly OrderDbContext _dbContext;

        public CancelOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(request.OrderId);

            if (order == null)
            {
                return false;
            }

            order.Status = OrderStatus.Canceled;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}