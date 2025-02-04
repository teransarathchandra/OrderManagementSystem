namespace Application.Commands.CreateOrder
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string ShippingAddress { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}