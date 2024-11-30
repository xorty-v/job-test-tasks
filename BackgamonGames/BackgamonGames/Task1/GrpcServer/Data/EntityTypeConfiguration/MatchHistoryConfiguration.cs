using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrpcServer.Data.EntityTypeConfiguration;

public class MatchHistoryConfiguration : IEntityTypeConfiguration<MatchHistory>
{
    public void Configure(EntityTypeBuilder<MatchHistory> builder)
    {
        builder.HasOne(mh => mh.FirstUser)
            .WithMany(u => u.MatchHistoriesFirstUser)
            .HasForeignKey(mh => mh.FirstUserId);

        builder.HasOne(mh => mh.SecondUser)
            .WithMany(u => u.MatchHistoriesSecondUser)
            .HasForeignKey(mh => mh.SecondUserId);

        builder.HasOne(mh => mh.WinnerUser)
            .WithMany(u => u.WonMatches)
            .HasForeignKey(mh => mh.WinnerId);
    }
}