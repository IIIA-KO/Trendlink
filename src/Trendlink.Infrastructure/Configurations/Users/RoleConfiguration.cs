using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(role => role.Id);

            builder.HasData(Role.Administrator);
            builder.HasData(Role.Registered);
        }
    }
}
