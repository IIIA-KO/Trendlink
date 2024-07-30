using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Shared;

namespace Trendlink.Infrastructure.Configurations.Cooperations
{
    internal sealed class CooperationConfiguration : IEntityTypeConfiguration<Cooperation>
    {
        public void Configure(EntityTypeBuilder<Cooperation> builder)
        {
            builder.ToTable("cooperations");

            builder.HasKey(cooperation => cooperation.Id);

            builder
                .Property(cooperation => cooperation.Id)
                .HasConversion(id => id.Value, value => new CooperationId(value));

            builder
                .Property(cooperation => cooperation.Name)
                .HasConversion(name => name.Value, value => new Name(value))
                .IsRequired();

            builder
                .Property(cooperation => cooperation.Description)
                .HasConversion(description => description.Value, value => new Description(value))
                .IsRequired();

            builder.Property(cooperation => cooperation.ScheduledOnUtc).IsRequired();

            builder
                .HasOne(cooperation => cooperation.Advertisement)
                .WithMany()
                .HasForeignKey(cooperation => cooperation.AdvertisementId)
                .IsRequired();

            builder
                .HasOne(cooperation => cooperation.Buyer)
                .WithMany()
                .HasForeignKey(cooperation => cooperation.BuyerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(cooperation => cooperation.Seller)
                .WithMany()
                .HasForeignKey(cooperation => cooperation.SellerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
