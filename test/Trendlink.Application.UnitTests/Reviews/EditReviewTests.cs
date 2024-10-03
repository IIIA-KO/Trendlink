using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Reviews.EditReview;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Reviews
{
    public class EditReviewTests
    {
        private static readonly EditReviewCommand Command =
            new(ReviewData.Create().Id, ReviewData.Rating.Value, ReviewData.Comment);

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly EditReviewCommandHandler _handler;

        public EditReviewTests()
        {
            this._reviewRepositoryMock = Substitute.For<IReviewRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new EditReviewCommandHandler(
                this._reviewRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenReviewNotFound()
        {
            // Arrange
            this._reviewRepositoryMock.GetByIdAsync(Command.ReviewId, Arg.Any<CancellationToken>())
                .Returns((Review?)null);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ReviewErrors.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotBuyer()
        {
            // Arrange
            Review review = ReviewData.Create();
            this._reviewRepositoryMock.GetByIdAsync(Command.ReviewId, Arg.Any<CancellationToken>())
                .Returns(review);

            this._userContextMock.UserId.Returns(ReviewData.SellerId);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenInvalidRatingProvided()
        {
            // Arrange
            Review review = ReviewData.Create();
            this._reviewRepositoryMock.GetByIdAsync(
                Arg.Any<ReviewId>(),
                Arg.Any<CancellationToken>()
            )
                .Returns(review);

            this._userContextMock.UserId.Returns(review.BuyerId);

            var invalidCommand = new EditReviewCommand(review.Id, -1, ReviewData.Comment);

            // Act
            Result result = await this._handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Rating.Invalid);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Review review = ReviewData.Create();
            this._reviewRepositoryMock.GetByIdAsync(Command.ReviewId, Arg.Any<CancellationToken>())
                .Returns(review);

            this._userContextMock.UserId.Returns(review.BuyerId);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            review.Rating.Should().Be(ReviewData.Rating);
            review.Comment.Should().Be(ReviewData.Comment);

            await this._unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
