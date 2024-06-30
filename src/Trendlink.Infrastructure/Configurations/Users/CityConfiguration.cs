using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users.Cities;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");

            builder.HasKey(city => city.Id);

            builder
                .Property(city => city.Id)
                .HasConversion(id => id.Value, value => new CityId(value));

            builder
                .Property(city => city.Name)
                .HasConversion(name => name.Value, value => new CityName(value));

            builder.Property(city => city.Name).IsRequired().HasMaxLength(200);

            builder
                .HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountyId)
                .IsRequired();
        }
    }
}
