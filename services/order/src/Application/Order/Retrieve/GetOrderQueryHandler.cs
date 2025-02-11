using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Retrieve
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Domain.Models.Order>
    {
        private readonly OrderDbContext _dbContext;

        public GetOrderQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Models.Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(o => o.Items) // Include related OrderItems
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        }
    }
}
