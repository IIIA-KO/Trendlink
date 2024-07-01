using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Configurations.Users
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.Id)
                .HasConversion(id => id.Value, value => new UserId(value));

            builder
                .Property(user => user.FirstName)
                .HasMaxLength(200)
                .HasConversion(firstName => firstName.Value, value => new FirstName(value));

            builder
                .Property(user => user.LastName)
                .HasMaxLength(200)
                .HasConversion(firstName => firstName.Value, value => new LastName(value));

            builder
                .Property(user => user.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Email(value));

            builder
                .Property(user => user.StateId)
                .HasConversion(stateId => stateId.Value, value => new StateId(value));

            builder
                .HasOne(user => user.State)
                .WithMany()
                .HasForeignKey(user => user.StateId)
                .IsRequired();

            builder
                .Property(user => user.PhoneNumber)
                .HasMaxLength(20)
                .HasConversion(phoneNumber => phoneNumber.Value, value => new PhoneNumber(value));

            builder
                .Property(user => user.Bio)
                .HasMaxLength(150)
                .HasConversion(bio => bio.Value, value => new Bio(value));

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasIndex(user => user.IdentityId).IsUnique();
        }
    }
}
