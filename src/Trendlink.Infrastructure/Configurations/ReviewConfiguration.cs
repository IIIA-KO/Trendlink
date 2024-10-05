using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Configurations
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");

            builder.HasIndex(review => review.Id);

            builder
                .Property(review => review.Id)
                .HasConversion(id => id.Value, value => new ReviewId(value));

            builder
                .Property(preview => preview.Rating)
                .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

            builder
                .Property(review => review.Comment)
                .HasConversion(comment => comment.Value, value => new Comment(value));

            builder
                .HasOne<Advertisement>()
                .WithMany()
                .HasForeignKey(review => review.AdvertisementId);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(review => review.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(review => review.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne<Cooperation>()
                .WithMany()
                .HasForeignKey(review => review.CooperationId)
                .IsRequired();
        }
    }
}
