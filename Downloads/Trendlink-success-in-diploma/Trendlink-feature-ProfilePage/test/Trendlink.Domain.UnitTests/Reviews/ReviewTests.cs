using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;

namespace Trendlink.Domain.UnitTests.Reviews
{
    public class ReviewTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues_WhenCooperationIsCompleted()
        {
            // Act
            Result<Review> reviewResult = Review.Create(
                ReviewData.CompletedCooperation,
                ReviewData.ValidRating,
                ReviewData.ValidComment,
                ReviewData.CreatedOnUtc
            );

            // Assert
            reviewResult.IsSuccess.Should().BeTrue();
            Review review = reviewResult.Value;

            review.BuyerId.Should().Be(ReviewData.CompletedCooperation.BuyerId);
            review.SellerId.Should().Be(ReviewData.CompletedCooperation.SellerId);
            review.AdvertisementId.Should().Be(ReviewData.CompletedCooperation.AdvertisementId);
            review.Rating.Should().Be(ReviewData.ValidRating);
            review.Comment.Should().Be(ReviewData.ValidComment);
            review.CreatedOnUtc.Should().Be(ReviewData.CreatedOnUtc);
        }

        [Fact]
        public void Create_Should_Fail_WhenCooperationIsNotCompleted()
        {
            // Act
            Result<Review> result = Review.Create(
                ReviewData.NotCompletedCooperation,
                ReviewData.ValidRating,
                ReviewData.ValidComment,
                ReviewData.CreatedOnUtc
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ReviewErrors.NotEligible);
        }

        [Fact]
        public void Update_Should_UpdateRatingAndComment_WhenValid()
        {
            // Arrange
            Result<Review> reviewResult = Review.Create(
                ReviewData.CompletedCooperation,
                ReviewData.ValidRating,
                ReviewData.ValidComment,
                ReviewData.CreatedOnUtc
            );

            Review review = reviewResult.Value;

            Rating newRating = Rating.Create(5).Value;
            var newComment = new Comment("Updated comment");

            // Act
            Result updateResult = review.Update(newRating, newComment);

            // Assert
            updateResult.IsSuccess.Should().BeTrue();
            review.Rating.Should().Be(newRating);
            review.Comment.Should().Be(newComment);
        }

        [Fact]
        public void Update_Should_Fail_WhenCommentIsInvalid()
        {
            // Arrange
            Result<Review> reviewResult = Review.Create(
                ReviewData.CompletedCooperation,
                ReviewData.ValidRating,
                ReviewData.ValidComment,
                ReviewData.CreatedOnUtc
            );

            Review review = reviewResult.Value;

            // Act
            Result result = review.Update(ReviewData.ValidRating, ReviewData.InvalidComment);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ReviewErrors.InvalidComment);
        }
    }
}
