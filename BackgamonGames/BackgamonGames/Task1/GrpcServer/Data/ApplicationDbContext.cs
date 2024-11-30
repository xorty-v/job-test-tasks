using GrpcServer.Data.EntityTypeConfiguration;
using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) => Database.Migrate();

    public DbSet<User> Users { get; set; }
    public DbSet<GameTransaction> GameTransactions { get; set; }
    public DbSet<MatchHistory> MatchHistories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MatchHistoryConfiguration());
        builder.ApplyConfiguration(new GameTransactionConfiguration());
        
        base.OnModelCreating(builder);
    }
}