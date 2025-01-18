using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public Guid CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
    }
}