using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Services;
using MediatR;

namespace Application.Order.Create
{
    public class CreateOrderCommandHandler(OrderDbContext dbContext, CatalogServiceClient catalogService, PaymentServiceClient paymentService) : IRequestHandler<CreateOrderCommand, Domain.Models.Order>
    {
        public async Task<Domain.Models.Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Fetch prices and validate product availability
            var orderItems = new List<OrderItem>();

            foreach (var item in request.OrderDto.Items)
            {
                // Validate product availability
                var isAvailable = await catalogService.CheckProductAvailabilityAsync(item.ProductId, item.Quantity);
                if (!isAvailable)
                {
                    throw new InvalidOperationException($"Product {item.ProductId} is not available in sufficient quantity.");
                }

                // Fetch product details (including price)
                var product = await catalogService.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product {item.ProductId} not found.");
                }

                // Add item to order
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                orderItems.Add(orderItem);
            }

            // Create order
            var order = new Domain.Models.Order
            {
                CustomerId = request.OrderDto.CustomerId,
                Items = orderItems,
                ShippingAddress = request.OrderDto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Reduce product quantities
            foreach (var item in orderItems)
            {
                try
                {
                    await catalogService.ReduceProductQuantityAsync(item.ProductId, item.Quantity);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Error reducing quantity for Product {item.ProductId}: {ex.Message}");
                }
            }

            //// Process payment
            //var paymentSuccessful = await _paymentService.ProcessPaymentAsync(order.Id, order.TotalAmount);
            //if (!paymentSuccessful)
            //{
            //    order.Status = "PaymentFailed";
            //    await _dbContext.SaveChangesAsync(cancellationToken);
            //    throw new InvalidOperationException("Payment failed. Order creation aborted.");
            //}

            order.Status = OrderStatus.Confirmed;
            await dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}
