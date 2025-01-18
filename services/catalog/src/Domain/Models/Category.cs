namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}