using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Reviews;
using Trendlink.Application.Reviews.GetReview;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;

namespace Trendlink.Application.UnitTests.Reviews
{
    public class GetReviewQueryHandlerTests
    {
        private static readonly GetReviewQuery Query = new GetReviewQuery(
            ReviewData.Create().Id // ID існуючого відгуку
        );

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly GetReviewQueryHandler _handler;

        public GetReviewQueryHandlerTests()
        {
            this._reviewRepositoryMock = Substitute.For<IReviewRepository>();
            this._handler = new GetReviewQueryHandler(this._reviewRepositoryMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenReviewNotFound()
        {
            // Arrange
            this._reviewRepositoryMock.GetByIdAsync(Query.ReviewId, Arg.Any<CancellationToken>())
                .Returns((Review?)null);

            // Act
            Result<ReviewResponse> result = await this._handler.Handle(
                Query,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ReviewErrors.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnReviewResponse_WhenReviewFound()
        {
            // Arrange
            Review review = ReviewData.Create();
            this._reviewRepositoryMock.GetByIdAsync(Query.ReviewId, Arg.Any<CancellationToken>())
                .Returns(review);

            // Act
            Result<ReviewResponse> result = await this._handler.Handle(
                Query,
                CancellationToken.None
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(review.Id.Value);
            result.Value.Rating.Should().Be(review.Rating.Value);
            result.Value.Comment.Should().Be(review.Comment.Value);
            result.Value.CreatedOnUtc.Should().Be(review.CreatedOnUtc);
        }
    }
}
