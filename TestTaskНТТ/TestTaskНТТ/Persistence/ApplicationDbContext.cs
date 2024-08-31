using Microsoft.EntityFrameworkCore;
using TestTaskНТТ.Models;
using TestTaskНТТ.Persistence.Configurations;

namespace TestTaskНТТ.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());

        base.OnModelCreating(builder);
    }
}