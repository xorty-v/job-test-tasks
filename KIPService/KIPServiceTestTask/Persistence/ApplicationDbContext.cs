using KIPServiceTestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) => Database.Migrate();

    public DbSet<QueryModel> Queries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}