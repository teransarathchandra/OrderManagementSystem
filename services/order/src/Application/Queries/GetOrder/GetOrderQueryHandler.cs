using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly OrderDbContext _dbContext;

        public GetOrderQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(o => o.Items) // Include related OrderItems
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        }
    }
}