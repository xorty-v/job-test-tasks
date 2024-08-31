using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskНТТ.Models;

namespace TestTaskНТТ.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.HasData(
            new Category { Id = 1, Name = "Электроника" },
            new Category { Id = 2, Name = "Одежда" },
            new Category { Id = 3, Name = "Книги" }
        );
    }
}