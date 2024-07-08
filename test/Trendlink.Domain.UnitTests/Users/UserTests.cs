using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetPropertyValue()
        {
            // Act
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.BirthDate.Should().Be(UserData.BirthDate);
            user.Email.Should().Be(UserData.Email);
            user.PhoneNumber.Should().Be(UserData.PhoneNumber);
            user.AccountType.Should().Be(AccountType.Personal);
            user.AccountCategory.Should().Be(AccountCategory.None);
            user.IdentityId.Should().Be(string.Empty);
        }

        [Fact]
        public void Create_Should_RaiseUserCreatedDomainEvent()
        {
            // Act
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Assert
            UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<
                UserCreatedDomainEvent,
                UserId
            >(user);

            userCreatedDomainEvent.UserId.Should().Be(user.Id);
        }

        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            // Act
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Assert
            user.Roles.Should().Contain(Role.Registered);
        }

        [Fact]
        public void Create_Should_Fail_WhenUserIsUnderage()
        {
            // Arrange
            var underageBirthDate = DateOnly.FromDateTime(
                DateTime.Now.AddYears(UserData.MinimumAge - 1)
            );

            // Act
            Result<User> result = User.Create(
                UserData.FirstName,
                UserData.LastName,
                underageBirthDate,
                UserData.Email,
                UserData.PhoneNumber
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.Underage);
        }

        [Fact]
        public void SetCity_Should_SetCityAndCityId()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Act
            user.SetState(UserData.State);

            // Assert
            user.State.Should().Be(UserData.State);
            user.StateId.Should().Be(UserData.State.Id);
        }

        [Fact]
        public void SetCity_Should_ThrowException_WhenCityIsNull()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Act
            Action setNullCity = () => user.SetState(null!);

            // Assert
            setNullCity.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetIdentityId_Should_SetIdentityId()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;
            string identityId = Guid.NewGuid().ToString();

            // Act
            user.SetIdentityId(identityId);

            // Assert
            user.IdentityId.Should().Be(identityId);
        }

        [Fact]
        public void SetIdentityId_Should_ThrowException_WhenIdentityIdIsNull()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Act
            Action setNullIdentityId = () => user.SetIdentityId(null!);

            // Assert
            setNullIdentityId.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_Should_UpdateUserProperties()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            var newFirstName = new FirstName("NewFirstName");
            var newLastName = new LastName("NewLastName");
            var newBirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));
            State newState = State
                .Create(
                    new StateName("NewState"),
                    Country.Create(new CountryName("NewCountry")).Value
                )
                .Value;
            var newBio = new Bio("New Bio");
            AccountType newAccountType = AccountType.Business;
            AccountCategory newAccountCategory = AccountCategory.Education;

            // Act
            user.Update(
                newFirstName,
                newLastName,
                newBirthDate,
                newState,
                newBio,
                newAccountType,
                newAccountCategory
            );

            // Assert
            user.FirstName.Should().Be(newFirstName);
            user.LastName.Should().Be(newLastName);
            user.BirthDate.Should().Be(newBirthDate);
            user.State.Should().Be(newState);
            user.StateId.Should().Be(newState.Id);
            user.Bio.Should().Be(newBio);
            user.AccountType.Should().Be(newAccountType);
            user.AccountCategory.Should().Be(newAccountCategory);
        }

        [Fact]
        public void Update_Should_Faile_WhenUserIsUnderage()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;
            var underageBirthDate = DateOnly.FromDateTime(
                DateTime.Now.AddYears(-UserData.MinimumAge + 1)
            );

            // Act
            Result result = user.Update(
                UserData.FirstName,
                UserData.LastName,
                underageBirthDate,
                UserData.State,
                UserData.Bio,
                AccountType.Personal,
                AccountCategory.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.Underage);
        }

        [Fact]
        public void AddRole_Should_AddRoleToUser_WhenRoleIsNotAlreadyPresent()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;

            // Act
            user.AddRole(Role.Administrator);

            // Assert
            user.Roles.Should().Contain(Role.Administrator);
        }

        [Fact]
        public void AddRole_Should_NotAddDuplicateRole()
        {
            // Arrange
            User user = User.Create(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber
            ).Value;
            user.AddRole(Role.Administrator);

            // Act
            user.AddRole(Role.Administrator);

            // Assert
            user.Roles.Count(r => r == Role.Administrator).Should().Be(1);
        }

        [Fact]
        public void IsOfLegalAge_Should_ReturnTrue_ForUserOlderThanMinimumAge()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(
                DateTime.UtcNow.AddYears(-UserData.MinimumAge - 1)
            );

            // Act
            bool isOfLegalAge = User.IsOfLegalAge(birthDate);

            // Assert
            isOfLegalAge.Should().BeTrue();
        }

        [Fact]
        public void IsOfLegalAge_Should_ReturnTrue_ForUserExactlyAtMinimumAge()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-UserData.MinimumAge));

            // Act
            bool isOfLegalAge = User.IsOfLegalAge(birthDate);

            // Assert
            isOfLegalAge.Should().BeTrue();
        }

        [Fact]
        public void IsOfLegalAge_Should_ReturnFalse_ForUserYoungerThanMinimumAge()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(
                DateTime.UtcNow.AddYears(-UserData.MinimumAge + 1)
            );

            // Act
            bool isOfLegalAge = User.IsOfLegalAge(birthDate);

            // Assert
            isOfLegalAge.Should().BeFalse();
        }

        [Fact]
        public void IsOfLegalAge_Should_ConsiderLeapYear()
        {
            // Arrange
            var referenceDate = new DateTime(2023, 2, 28, 23, 59, 59, DateTimeKind.Utc);
            var birthDate = DateOnly.FromDateTime(referenceDate);

            // Act
            bool isOfLegalAge = User.IsOfLegalAge(birthDate);

            // Assert
            isOfLegalAge.Should().BeFalse();
        }
    }
}
