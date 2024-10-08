﻿using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users;
using Trendlink.Application.Users.GetUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class GetUserTests
    {
        public static readonly GetUserQuery Query = new(UserData.Create().Id);

        private readonly IUserRepository _userRepositoryMock;

        private readonly GetUserQueryHandler _handler;

        public GetUserTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();

            this._handler = new GetUserQueryHandler(this._userRepositoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserNotFound()
        {
            // Arrange
            this._userRepositoryMock.GetByIdWithStateAsync(
                Query.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns((User?)null);

            // Act
            Result<UserResponse> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }
    }
}
