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
            // Validate product availability
            foreach (var item in request.OrderDto.Items)
            {
                var isAvailable = await _catalogService.CheckProductAvailabilityAsync(item.ProductId, item.Quantity);
                if (!isAvailable)
                {
                    throw new InvalidOperationException($"Product {item.ProductId} is not available in sufficient quantity.");
                }
            }

            // Create order
            var order = new Order
            {
                CustomerId = request.OrderDto.CustomerId,
                Items = request.OrderDto.Items.Select(dto => new OrderItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    Price = dto.Price
                }).ToList(),
                TotalAmount = request.OrderDto.Items.Sum(i => i.Quantity * i.Price),
                ShippingAddress = request.OrderDto.ShippingAddress,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var item in request.OrderDto.Items)
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

            order.Status = "Confirmed";
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}