using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}