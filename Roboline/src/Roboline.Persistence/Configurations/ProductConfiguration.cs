using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roboline.Domain;

namespace Roboline.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Name).IsRequired();

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasData(
            new Product
            {
                Id = 1, Name = "Smartphone", Description = "Latest model smartphone", Price = 999.99m, CategoryId = 1
            },
            new Product
            {
                Id = 2, Name = "Laptop", Description = "High-performance laptop", Price = 1500.50m, CategoryId = 1
            },
            new Product
            {
                Id = 3, Name = "Tablet", Description = "Lightweight tablet", Price = 600.00m, CategoryId = 1
            },
            new Product
            {
                Id = 4, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m,
                CategoryId = 1
            },
            new Product
            {
                Id = 5, Name = "Camera", Description = "Professional digital camera", Price = 1200.00m, CategoryId = 1
            },
            new Product
            {
                Id = 6, Name = "Fiction Novel", Description = "Best-selling fiction novel", Price = 20.99m,
                CategoryId = 2
            },
            new Product
            {
                Id = 7, Name = "Science Textbook", Description = "Comprehensive science textbook", Price = 50.00m,
                CategoryId = 2
            },
            new Product
            {
                Id = 8, Name = "Historical Biography", Description = "Life of famous historical figure", Price = 25.75m,
                CategoryId = 2
            },
            new Product
            {
                Id = 9, Name = "Children's Book", Description = "Popular children's storybook", Price = 15.49m,
                CategoryId = 2
            },
            new Product
            {
                Id = 10, Name = "Cookbook", Description = "Recipes from world-renowned chefs", Price = 30.00m,
                CategoryId = 2
            },
            new Product
            {
                Id = 11, Name = "Men's T-Shirt", Description = "Casual cotton t-shirt", Price = 10.00m, CategoryId = 3
            },
            new Product
            {
                Id = 12, Name = "Women's Dress", Description = "Elegant evening dress", Price = 75.00m, CategoryId = 3
            },
            new Product
            {
                Id = 13, Name = "Children's Jacket", Description = "Winter jacket for kids", Price = 50.00m,
                CategoryId = 3
            },
            new Product
            {
                Id = 14, Name = "Men's Jeans", Description = "Denim jeans for men", Price = 40.00m, CategoryId = 3
            },
            new Product
            {
                Id = 15, Name = "Women's Hat", Description = "Stylish summer hat", Price = 20.00m, CategoryId = 3
            },
            new Product
            {
                Id = 16, Name = "Blender", Description = "High-speed kitchen blender", Price = 100.00m, CategoryId = 4
            },
            new Product
            {
                Id = 17, Name = "Microwave Oven", Description = "Compact microwave oven", Price = 150.00m,
                CategoryId = 4
            },
            new Product
            {
                Id = 18, Name = "Cookware Set", Description = "Non-stick cookware set", Price = 120.00m, CategoryId = 4
            },
            new Product
            {
                Id = 19, Name = "Toaster", Description = "Four-slice toaster", Price = 30.00m, CategoryId = 4
            },
            new Product
            {
                Id = 20, Name = "Kids Building Blocks", Description = "Creative building block set", Price = 25.00m,
                CategoryId = 5
            }
        );
    }
}