using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Configurations
{
    internal sealed class ConditionConfiguration : IEntityTypeConfiguration<Condition>
    {
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder.ToTable("conditions");

            builder.HasKey(condition => condition.Id);

            builder
                .Property(condition => condition.Id)
                .HasConversion(conditionId => conditionId.Value, value => new ConditionId(value))
                .IsRequired();

            builder
                .Property(condition => condition.Description)
                .HasConversion(description => description.Value, value => new Description(value))
                .IsRequired();

            builder
                .Property(condition => condition.UserId)
                .HasConversion(userId => userId.Value, value => new UserId(value))
                .IsRequired();

            builder
                .HasMany(condition => condition.Advertisements)
                .WithOne(advertisement => advertisement.Condition)
                .HasForeignKey(advertisement => advertisement.ConditionId)
                .IsRequired();
        }
    }
}
