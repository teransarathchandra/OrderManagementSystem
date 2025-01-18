namespace Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        PaymentFailed,
        Shipped,
        Delivered,
        Canceled
    }
}