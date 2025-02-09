namespace Application.Order.Create
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string ShippingAddress { get; set; }
    }
}
