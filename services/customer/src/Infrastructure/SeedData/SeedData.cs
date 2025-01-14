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
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main St, Cityville",
                },
                new Customer
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm St, Townville",
                },
                new Customer
                {
                    Id = 3,
                    Name = "Alice Johnson",
                    Email = "alice.johnson@example.com",
                    Address = "789 Oak St, Villageville",
                }
            );
        }
    }
}
