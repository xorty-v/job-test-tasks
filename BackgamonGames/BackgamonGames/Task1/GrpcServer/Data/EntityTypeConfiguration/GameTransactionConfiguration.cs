using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrpcServer.Data.EntityTypeConfiguration;

public class GameTransactionConfiguration : IEntityTypeConfiguration<GameTransaction>
{
    public void Configure(EntityTypeBuilder<GameTransaction> builder)
    {
        builder.HasOne(gt => gt.Sender)
            .WithMany(u => u.SenderTransactions)
            .HasForeignKey(gt => gt.SenderId);

        builder.HasOne(gt => gt.Receiver)
            .WithMany(u => u.ReceiverTransactions)
            .HasForeignKey(gt => gt.ReceiverId);
    }
}