using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            var electronicsCategoryId = Guid.Parse("1d2aac20-7887-4fd2-9327-c637e88676cd");
            var booksCategoryId = Guid.Parse("06410596-67dd-4cb1-9bc3-2ac75688cd59");
            var clothingCategoryId = Guid.Parse("d794d57d-7d26-4303-a528-3f1fbfd55947");

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = electronicsCategoryId, Name = "Electronics", Description = "Electronic gadgets and devices" },
                new Category { Id = booksCategoryId, Name = "Books", Description = "Books and literature" },
                new Category { Id = clothingCategoryId, Name = "Clothing", Description = "Apparel and accessories" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("88195839-6d90-4a13-9d99-435ddd95f9f6"),
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 699.99M,
                    AvailableQuantity = 50,
                    CategoryId = electronicsCategoryId // Mapping to Electronics
                },
                new Product
                {
                    Id = Guid.Parse("8ffa522c-f63d-46e5-a33c-199d733ce9e9"),
                    Name = "Laptop",
                    Description = "High-performance laptop",
                    Price = 999.99M,
                    AvailableQuantity = 30,
                    CategoryId = electronicsCategoryId // Mapping to Electronics
                },
                new Product
                {
                    Id = Guid.Parse("cdb43089-8946-405b-9f16-dac300dda55f"),
                    Name = "Fiction Novel",
                    Description = "Best-selling fiction book",
                    Price = 19.99M,
                    AvailableQuantity = 100,
                    CategoryId = booksCategoryId // Mapping to Books
                },
                new Product
                {
                    Id = Guid.Parse("554620cf-426f-431b-bf6c-49dcb6337e73"),
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 14.99M,
                    AvailableQuantity = 200,
                    CategoryId = clothingCategoryId // Mapping to Clothing
                }
            );
        }
    }
}