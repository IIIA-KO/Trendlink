using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;

namespace Trendlink.Infrastructure.Configurations
{
    internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");

            builder.HasKey(notification => notification.Id);

            builder
                .Property(notification => notification.Id)
                .HasConversion(id => id.Value, value => new NotificationId(value))
                .IsRequired();

            builder
                .Property(notification => notification.Title)
                .HasConversion(title => title.Value, value => new Title(value))
                .IsRequired();

            builder
                .Property(notification => notification.Message)
                .HasConversion(message => message.Value, value => new Message(value))
                .IsRequired();

            builder
                .HasOne(notification => notification.User)
                .WithMany(user => user.Notifications)
                .HasForeignKey(notification => notification.UserId)
                .IsRequired();
        }
    }
}
