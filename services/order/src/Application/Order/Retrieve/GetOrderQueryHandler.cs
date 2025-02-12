using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Retrieve
{
    public class GetOrderQueryHandler(OrderDbContext dbContext) : IRequestHandler<GetOrderQuery, Domain.Models.Order>
    {
        public async Task<Domain.Models.Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .Include(o => o.Items) // Include related OrderItems
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        }
    }
}
