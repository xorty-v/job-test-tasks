using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roboline.Domain;

namespace Roboline.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Name).IsRequired();

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasData(
            new ProductCategory { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets." },
            new ProductCategory { Id = 2, Name = "Books", Description = "All genres of books and literature." },
            new ProductCategory { Id = 3, Name = "Clothing", Description = "Apparel for men, women, and children." },
            new ProductCategory { Id = 4, Name = "Home & Kitchen", Description = "Appliances and utensils for home." },
            new ProductCategory { Id = 5, Name = "Toys", Description = "Toys for kids of all ages." }
        );
    }
}