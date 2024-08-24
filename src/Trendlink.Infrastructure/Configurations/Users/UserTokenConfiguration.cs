using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("user_tokens");

            builder.HasKey(userToken => userToken.Id);

            builder
                .Property(userToken => userToken.UserId)
                .HasConversion(userId => userId.Value, value => new UserId(value))
                .IsRequired();
        }
    }
}
