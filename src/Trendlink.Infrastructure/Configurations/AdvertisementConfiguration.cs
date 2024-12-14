using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Infrastructure.Configurations
{
    internal sealed class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable("advertisements");

            builder.HasKey(advertisement => advertisement.Id);

            builder
                .Property(advertisement => advertisement.Id)
                .HasConversion(id => id.Value, value => new AdvertisementId(value));

            builder
                .Property(advertisement => advertisement.Name)
                .HasConversion(name => name.Value, value => new Name(value))
                .IsRequired();

            builder
                .Property(advertisement => advertisement.Description)
                .HasConversion(description => description.Value, value => new Description(value))
                .IsRequired();

            builder
                .Property(advertisement => advertisement.ConditionId)
                .HasConversion(conditionId => conditionId.Value, value => new ConditionId(value));

            builder
                .HasOne(advertisement => advertisement.Condition)
                .WithMany(condition => condition.Advertisements)
                .HasForeignKey(advertisement => advertisement.ConditionId)
                .IsRequired();

            builder.OwnsOne(
                advertisement => advertisement.Price,
                priceBuilder =>
                    priceBuilder
                        .Property(money => money.Currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code))
                        .IsRequired()
            );

            builder.Property<uint>("Version").IsRowVersion();
        }
    }
}
