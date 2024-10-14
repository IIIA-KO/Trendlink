using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Reviews.DeleteReview;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Reviews
{
    public class DeleteReviewTests
    {
        private static readonly DeleteReviewCommand Command = new DeleteReviewCommand(
            ReviewData.Create().Id
        );

        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly DeleteReviewCommandHandler _handler;

        public DeleteReviewTests()
        {
            this._reviewRepositoryMock = Substitute.For<IReviewRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new DeleteReviewCommandHandler(
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
        public async Task Handle_ShouldReturnSuccess_WhenReviewDeleted()
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

            this._reviewRepositoryMock.Received(1).Remove(Arg.Is<Review>(r => r.Id == review.Id));

            await this._unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
