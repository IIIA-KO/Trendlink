using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");

            builder.HasKey(country => country.Id);

            builder
                .Property(country => country.Id)
                .HasConversion(id => id.Value, value => new CountryId(value));

            builder
                .Property(country => country.Name)
                .HasConversion(name => name.Value, value => new CountryName(value))
                .IsRequired();

            builder.Property(country => country.Name).IsRequired().HasMaxLength(200);

            builder
                .HasMany(country => country.Cities)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountyId)
                .IsRequired();
        }
    }
}
