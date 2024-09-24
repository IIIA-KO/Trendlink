using FluentAssertions;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Photos;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.Photos.SetProfilePhoto;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class SetProfilePhotoTests
    {
        private static readonly SetProfilePhotoCommand Command = new(Substitute.For<IFormFile>());

        private readonly IPhotoAccessor _photoAccessorMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly SetProfilePhotoCommandHandler _handler;

        public SetProfilePhotoTests()
        {
            this._photoAccessorMock = Substitute.For<IPhotoAccessor>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new SetProfilePhotoCommandHandler(
                this._photoAccessorMock,
                this._userRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAddPhotoFails()
        {
            // Arrange
            var failureResult = Result.Failure<Photo>(PhotoErrors.FailedUpload);

            this._photoAccessorMock.AddPhotoAsync(Command.File).Returns(failureResult);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PhotoErrors.FailedUpload);
            await this
                ._unitOfWorkMock.DidNotReceive()
                .SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Should_DeleteExistingPhoto_WhenUserHasProfilePhoto()
        {
            // Arrange
            Photo existingPhoto = UserData.ProfilePhoto;

            User user = UserData.Create();
            user.SetProfilePhoto(existingPhoto);

            Photo newPhoto = user.ProfilePhoto! with { Id = "new-photo-id" };

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            this._photoAccessorMock.AddPhotoAsync(Command.File).Returns(Result.Success(newPhoto));
            this._photoAccessorMock.DeletePhotoAsync(existingPhoto.Id).Returns(Result.Success());

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            user.ProfilePhoto.Should().Be(newPhoto);
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

            Photo newPhoto = user.ProfilePhoto! with { Id = "new-photo-id" };

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            this._photoAccessorMock.AddPhotoAsync(Command.File).Returns(Result.Success(newPhoto));
            this._photoAccessorMock.DeletePhotoAsync(existingPhoto.Id)
                .Returns(Result.Failure(PhotoErrors.FailedDelete));

            // Act
            var command = new SetProfilePhotoCommand(Command.File);
            Result result = await this._handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PhotoErrors.FailedDelete);
            await this._photoAccessorMock.Received(1).DeletePhotoAsync(existingPhoto.Id);
            await this
                ._unitOfWorkMock.DidNotReceive()
                .SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            User user = UserData.Create();

            Photo photo = UserData.ProfilePhoto;

            this._userContextMock.UserId.Returns(user.Id);

            this._userRepositoryMock.GetByIdAsync(user.Id, default).Returns(user);

            this._photoAccessorMock.AddPhotoAsync(Command.File).Returns(Result.Success(photo));

            // Act
            var command = new SetProfilePhotoCommand(Command.File);
            Result result = await this._handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            user.ProfilePhoto.Should().Be(photo);
            await this._unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
