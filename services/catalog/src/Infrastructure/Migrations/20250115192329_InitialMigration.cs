using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("06410596-67dd-4cb1-9bc3-2ac75688cd59"), "Books and literature", "Books" },
                    { new Guid("1d2aac20-7887-4fd2-9327-c637e88676cd"), "Electronic gadgets and devices", "Electronics" },
                    { new Guid("d794d57d-7d26-4303-a528-3f1fbfd55947"), "Apparel and accessories", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("554620cf-426f-431b-bf6c-49dcb6337e73"), 200, new Guid("d794d57d-7d26-4303-a528-3f1fbfd55947"), "Comfortable cotton t-shirt", "T-Shirt", 14.99m },
                    { new Guid("88195839-6d90-4a13-9d99-435ddd95f9f6"), 50, new Guid("1d2aac20-7887-4fd2-9327-c637e88676cd"), "Latest model smartphone", "Smartphone", 699.99m },
                    { new Guid("8ffa522c-f63d-46e5-a33c-199d733ce9e9"), 30, new Guid("1d2aac20-7887-4fd2-9327-c637e88676cd"), "High-performance laptop", "Laptop", 999.99m },
                    { new Guid("cdb43089-8946-405b-9f16-dac300dda55f"), 100, new Guid("06410596-67dd-4cb1-9bc3-2ac75688cd59"), "Best-selling fiction book", "Fiction Novel", 19.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
