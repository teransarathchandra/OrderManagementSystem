using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Table Name
            builder.ToTable("OrderItems");

            // Composite Key
            builder.HasKey(oi => new { oi.ProductId, oi.OrderId });

            // Properties
            builder.Property(oi => oi.ProductId)
                .IsRequired();

            builder.Property(oi => oi.OrderId)
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .IsRequired();
        }
    }
}