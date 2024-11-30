using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Roboline.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic devices and gadgets.", "Electronics" },
                    { 2, "All genres of books and literature.", "Books" },
                    { 3, "Apparel for men, women, and children.", "Clothing" },
                    { 4, "Appliances and utensils for home.", "Home & Kitchen" },
                    { 5, "Toys for kids of all ages.", "Toys" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Latest model smartphone", "Smartphone", 999.99m },
                    { 2, 1, "High-performance laptop", "Laptop", 1500.50m },
                    { 3, 1, "Lightweight tablet", "Tablet", 600.00m },
                    { 4, 1, "Noise-cancelling headphones", "Headphones", 199.99m },
                    { 5, 1, "Professional digital camera", "Camera", 1200.00m },
                    { 6, 2, "Best-selling fiction novel", "Fiction Novel", 20.99m },
                    { 7, 2, "Comprehensive science textbook", "Science Textbook", 50.00m },
                    { 8, 2, "Life of famous historical figure", "Historical Biography", 25.75m },
                    { 9, 2, "Popular children's storybook", "Children's Book", 15.49m },
                    { 10, 2, "Recipes from world-renowned chefs", "Cookbook", 30.00m },
                    { 11, 3, "Casual cotton t-shirt", "Men's T-Shirt", 10.00m },
                    { 12, 3, "Elegant evening dress", "Women's Dress", 75.00m },
                    { 13, 3, "Winter jacket for kids", "Children's Jacket", 50.00m },
                    { 14, 3, "Denim jeans for men", "Men's Jeans", 40.00m },
                    { 15, 3, "Stylish summer hat", "Women's Hat", 20.00m },
                    { 16, 4, "High-speed kitchen blender", "Blender", 100.00m },
                    { 17, 4, "Compact microwave oven", "Microwave Oven", 150.00m },
                    { 18, 4, "Non-stick cookware set", "Cookware Set", 120.00m },
                    { 19, 4, "Four-slice toaster", "Toaster", 30.00m },
                    { 20, 5, "Creative building block set", "Kids Building Blocks", 25.00m }
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
                name: "ProductCategories");
        }
    }
}
