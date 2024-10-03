using Microsoft.VisualBasic;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Review.DomainEvents;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Review
{
    public sealed class Review : Entity<ReviewId>
    {
        public Review(
            ReviewId id,
            UserId userId,
            AdvertisementId advertisementId,
            CooperationId cooperationId,
            Rating rating,
            Comment comment,
            DateTime createdOnUtc
        )
            : base(id)
        {
            this.UserId = userId;
            this.AdvertisementId = advertisementId;
            this.CooperationId = cooperationId;
            this.Rating = rating;
            this.Comment = comment;
            this.CreatedOnUtc = createdOnUtc;
        }

        private Review() { }

        public UserId UserId { get; private set; }

        public AdvertisementId AdvertisementId { get; private set; }

        public CooperationId CooperationId { get; private set; }

        public Comment Comment { get; private set; }

        public Rating Rating { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public static Result<Review> Create(
            Cooperation cooperation,
            Rating rating,
            Comment comment,
            DateTime createdOnUtc
        )
        {
            if (cooperation.Status != CooperationStatus.Completed)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            var review = new Review(
                ReviewId.New(),
                cooperation.BuyerId,
                cooperation.AdvertisementId,
                cooperation.Id,
                rating,
                comment,
                createdOnUtc
            );

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }

        public Result Update(Rating rating, Comment comment)
        {
            if (string.IsNullOrEmpty(comment.Value))
            {
                return Result.Failure(ReviewErrors.InvalidComment);
            }

            this.Rating = rating;
            this.Comment = comment;

            return Result.Success();
        }
    }
}
