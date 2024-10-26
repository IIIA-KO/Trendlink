using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Reviews.CreateReview;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Reviews
{
    public class CreateReviewTests
    {
        private static readonly CreateReviewCommand Command = new CreateReviewCommand(
            ReviewData.Rating.Value,
            ReviewData.Cooperation.Id,
            ReviewData.Comment
        );

        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IReviewRepository _reviewRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CreateReviewCommandHandler _handler;

        public CreateReviewTests()
        {
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._reviewRepositoryMock = Substitute.For<IReviewRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(ReviewData.UtcNow);

            this._handler = new CreateReviewCommandHandler(
                this._cooperationRepositoryMock,
                this._reviewRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock,
                dateTimeProvider
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCooperationNotFound()
        {
            // Arrange
            this._cooperationRepositoryMock.GetByIdAsync(
                Command.CooperationId,
                Arg.Any<CancellationToken>()
            )
                .Returns((Cooperation?)null);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotBuyer()
        {
            // Arrange
            Cooperation cooperation = ReviewData.Cooperation;
            this._cooperationRepositoryMock.GetByIdAsync(
                Command.CooperationId,
                Arg.Any<CancellationToken>()
            )
                .Returns(cooperation);

            this._userContextMock.UserId.Returns(ReviewData.SellerId); // Не покупець

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenReviewAlreadyExists()
        {
            // Arrange
            Cooperation cooperation = ReviewData.Cooperation;
            this._cooperationRepositoryMock.GetByIdAsync(
                Command.CooperationId,
                Arg.Any<CancellationToken>()
            )
                .Returns(cooperation);

            this._userContextMock.UserId.Returns(cooperation.BuyerId);

            this._reviewRepositoryMock.GetByCooperationIdAndBuyerIdAsync(
                Command.CooperationId,
                Arg.Any<UserId>(),
                Arg.Any<CancellationToken>()
            )
                .Returns(ReviewData.Create());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ReviewErrors.AlreadyReviewed);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Cooperation cooperation = ReviewData.Cooperation;
            this._cooperationRepositoryMock.GetByIdAsync(
                Command.CooperationId,
                Arg.Any<CancellationToken>()
            )
                .Returns(cooperation);
            this._userContextMock.UserId.Returns(cooperation.BuyerId);

            this._reviewRepositoryMock.GetByCooperationIdAndBuyerIdAsync(
                Command.CooperationId,
                ReviewData.BuyerId,
                Arg.Any<CancellationToken>()
            )
                .Returns((Review?)null); // Відгуку немає

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            this._reviewRepositoryMock.Received(1)
                .Add(
                    Arg.Is<Review>(r =>
                        r.CooperationId == cooperation.Id
                        && r.Rating == ReviewData.Rating
                        && r.Comment == ReviewData.Comment
                        && r.CreatedOnUtc == ReviewData.UtcNow
                    )
                );

            await this._unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
