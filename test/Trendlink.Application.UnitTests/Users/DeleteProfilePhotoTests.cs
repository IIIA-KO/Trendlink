using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Photos;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.Photos.DeleteProfilePhoto;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class DeleteProfilePhotoTests
    {
        private readonly IPhotoAccessor _photoAccessorMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly DeleteProfilePhotoCommandHandler _handler;

        public DeleteProfilePhotoTests()
        {
            this._photoAccessorMock = Substitute.For<IPhotoAccessor>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new DeleteProfilePhotoCommandHandler(
                this._photoAccessorMock,
                this._userRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserHasNoProfilePhoto()
        {
            // Arrange
            User user = UserData.Create();

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            // Act
            Result result = await this._handler.Handle(
                new DeleteProfilePhotoCommand(),
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PhotoErrors.PhotoNotFound);
            await this._photoAccessorMock.DidNotReceive().DeletePhotoAsync(Arg.Any<string>());
            await this
                ._unitOfWorkMock.DidNotReceive()
                .SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Should_DeletePhoto_WhenUserHasProfilePhoto()
        {
            // Arrange
            Photo existingPhoto = UserData.ProfilePhoto;

            User user = UserData.Create();
            user.SetProfilePhoto(existingPhoto);

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            this._photoAccessorMock.DeletePhotoAsync(existingPhoto.Id).Returns(Result.Success());

            // Act
            Result result = await this._handler.Handle(
                new DeleteProfilePhotoCommand(),
                CancellationToken.None
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            user.ProfilePhoto.Should().BeNull();
            await this._photoAccessorMock.Received(1).DeletePhotoAsync(existingPhoto.Id);
            await this._unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenDeletePhotoFails()
        {
            // Arrange
            Photo existingPhoto = UserData.ProfilePhoto;

            User user = UserData.Create();
            user.SetProfilePhoto(existingPhoto);

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            this._photoAccessorMock.DeletePhotoAsync(existingPhoto.Id)
                .Returns(Result.Failure(PhotoErrors.FailedDelete));

            // Act
            Result result = await this._handler.Handle(
                new DeleteProfilePhotoCommand(),
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PhotoErrors.FailedDelete);
            user.ProfilePhoto.Should().Be(existingPhoto);
            await this._photoAccessorMock.Received(1).DeletePhotoAsync(existingPhoto.Id);
            await this
                ._unitOfWorkMock.DidNotReceive()
                .SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
