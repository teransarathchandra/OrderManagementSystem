using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table Name
            builder.ToTable("Orders");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.ShippingAddress)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<int>() // Converts enum to integer
                .IsRequired();

            // Relationships
            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}