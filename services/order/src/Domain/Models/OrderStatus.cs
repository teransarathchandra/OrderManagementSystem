namespace Domain.Models
{
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