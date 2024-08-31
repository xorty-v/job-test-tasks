using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskНТТ.Models;

namespace TestTaskНТТ.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).HasMaxLength(75).IsRequired();

        builder.HasData(
            new Product { Id = 1, Name = "Ноутбук", Price = 29999.99, CategoryId = 1 },
            new Product { Id = 2, Name = "Смартфон", Price = 19999.99, CategoryId = 1 },
            new Product { Id = 3, Name = "Футболка", Price = 1000.00, CategoryId = 2 },
            new Product { Id = 4, Name = "Джинсы", Price = 4999.99, CategoryId = 2 },
            new Product { Id = 5, Name = "Книга по программированию", Price = 800.00, CategoryId = 3 },
            new Product { Id = 6, Name = "Роман", Price = 400.00, CategoryId = 3 }
        );
    }
}