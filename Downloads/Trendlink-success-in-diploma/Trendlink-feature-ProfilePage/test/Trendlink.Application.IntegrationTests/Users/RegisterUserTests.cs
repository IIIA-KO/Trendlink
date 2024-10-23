using FluentAssertions;
using Trendlink.Application.Accounts.Register;
using Trendlink.Application.IntegrationTests.Infrastructure;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.IntegrationTests.Users
{
    public class RegisterUserTests : BaseIntegrationTest
    {
        public RegisterUserTests(IntegrationTestWebAppFactory factory)
            : base(factory) { }

        [Fact]
        public async Task RegisterUser_Should_ReturnFaulire_When_StateIsNull()
        {
            // Arrange
            User user = UserData.Create();

            var command = new RegisterCommand(
                user.FirstName,
                user.LastName,
                user.BirthDate,
                user.Email,
                user.PhoneNumber,
                UserData.Password,
                StateId.New()
            );

            // Act
            Result<UserId> result = await this.Sender.Send(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.NotFound);
        }
    }
}
