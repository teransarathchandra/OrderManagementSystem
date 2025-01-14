using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices" },
                new Category { Id = 2, Name = "Books", Description = "Books and literature" },
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and accessories" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 699.99M,
                    AvailableQuantity = 50,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Description = "High-performance laptop",
                    Price = 999.99M,
                    AvailableQuantity = 30,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Fiction Novel",
                    Description = "Best-selling fiction book",
                    Price = 19.99M,
                    AvailableQuantity = 100,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 14.99M,
                    AvailableQuantity = 200,
                    CategoryId = 3
                }
            );
        }
    }
}