using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Cooperations.BlockedDates;

namespace Trendlink.Infrastructure.Configurations.Cooperations
{
    internal sealed class BlockedDateConfiguration : IEntityTypeConfiguration<BlockedDate>
    {
        public void Configure(EntityTypeBuilder<BlockedDate> builder)
        {
            builder.ToTable("blocked_dates");

            builder.HasKey(blockedDate => blockedDate.Id);

            builder
                .Property(blockedDate => blockedDate.Id)
                .HasConversion(id => id.Value, value => new BlockedDateId(value));

            builder
                .Property(blockedDate => blockedDate.UserId)
                .HasConversion(userId => userId.Value, value => new UserId(value))
                .IsRequired();

            builder.Property(blockedDate => blockedDate.Date).IsRequired();

            builder
                .HasOne(blockedDate => blockedDate.User)
                .WithMany()
                .HasForeignKey(blockedDate => blockedDate.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(blockedDate => new { blockedDate.UserId, blockedDate.Date })
                .IsUnique();
        }
    }
}
