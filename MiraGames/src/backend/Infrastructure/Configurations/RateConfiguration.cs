using backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Configurations;

internal sealed class RateConfiguration : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Value)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder.Property(r => r.UpdatedAtOnUtc)
            .IsRequired();
    }
}