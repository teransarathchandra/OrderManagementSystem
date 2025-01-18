using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Services;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly OrderDbContext _dbContext;
        private readonly CatalogServiceClient _catalogService;
        //private readonly PaymentServiceClient _paymentService;

        public CreateOrderHandler(OrderDbContext dbContext, CatalogServiceClient catalogService, PaymentServiceClient paymentService)
        {
            _dbContext = dbContext;
            _catalogService = catalogService;
            //_paymentService = paymentService;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Fetch prices and validate product availability
            var orderItems = new List<OrderItem>();

            foreach (var item in request.OrderDto.Items)
            {
                // Validate product availability
                var isAvailable = await _catalogService.CheckProductAvailabilityAsync(item.ProductId, item.Quantity);
                if (!isAvailable)
                {
                    throw new InvalidOperationException($"Product {item.ProductId} is not available in sufficient quantity.");
                }

                // Fetch product details (including price)
                var product = await _catalogService.GetProductByIdAsync(item.ProductId);
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
            var order = new Order
            {
                CustomerId = request.OrderDto.CustomerId,
                Items = orderItems,
                ShippingAddress = request.OrderDto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Reduce product quantities
            foreach (var item in orderItems)
            {
                try
                {
                    await _catalogService.ReduceProductQuantityAsync(item.ProductId, item.Quantity);
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
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}