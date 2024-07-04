using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users.States;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("states");

            builder.HasKey(state => state.Id);

            builder
                .Property(state => state.Id)
                .HasConversion(id => id.Value, value => new StateId(value));

            builder
                .Property(state => state.Name)
                .HasConversion(name => name.Value, value => new StateName(value));

            builder.Property(state => state.Name).IsRequired().HasMaxLength(200);

            builder
                .HasOne(state => state.Country)
                .WithMany(country => country.States)
                .HasForeignKey(state => state.CountryId)
                .IsRequired();
        }
    }
}
