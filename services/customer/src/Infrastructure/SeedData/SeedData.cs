using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-e1f2a3b4c5d6"),
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main St, Cityville",
                },
                new Customer
                {
                    Id = Guid.Parse("a2b3c4d5-e6f7-8a9b-0c1d-f2a3b4c5d6e7"),
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm St, Townville",
                },
                new Customer
                {
                    Id = Guid.Parse("a3b4c5d6-e7f8-9a0b-1c2d-f3a4b5c6d7e8"),
                    Name = "Alice Johnson",
                    Email = "alice.johnson@example.com",
                    Address = "789 Oak St, Villageville",
                }
            );
        }
    }
}
