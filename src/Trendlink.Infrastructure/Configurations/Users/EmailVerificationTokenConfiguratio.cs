using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class EmailVerificationTokenConfiguratio
        : IEntityTypeConfiguration<EmailVerificationToken>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
        {
            builder.ToTable("email_verification_tokens");

            builder.HasKey(token => token.Id);

            builder
                .Property(token => token.Id)
                .HasConversion(id => id.Value, value => new EmailVerificationTokenId(value));

            builder.HasOne(token => token.User).WithMany().HasForeignKey(token => token.UserId);
        }
    }
}
