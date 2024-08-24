using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Infrastructure.Configurations.Users
{
    public class InstagramAccountConfiguration : IEntityTypeConfiguration<InstagramAccount>
    {
        public void Configure(EntityTypeBuilder<InstagramAccount> builder)
        {
            builder.ToTable("instagram_accounts");

            builder.HasKey(instagramAccount => instagramAccount.Id);

            builder
                .Property(instagramAccount => instagramAccount.Id)
                .HasConversion(
                    instagramAccountId => instagramAccountId.Value,
                    value => new InstagramAccountId(value)
                );

            builder
                .Property(instagramAccount => instagramAccount.FacebookPageId)
                .HasConversion(
                    facebookPageId => facebookPageId.Value,
                    value => new FacebookPageId(value)
                )
                .IsRequired();

            builder
                .Property(instagramAccount => instagramAccount.UserId)
                .HasConversion(
                    instagramAccount => instagramAccount.Value,
                    value => new UserId(value)
                )
                .IsRequired();

            builder.OwnsOne(instagramAccount => instagramAccount.Metadata);
        }
    }
}
