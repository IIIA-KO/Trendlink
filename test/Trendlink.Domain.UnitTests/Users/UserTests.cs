﻿using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.UnitTests.Users
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetPropertyValue()
        {
            // Act
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

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
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

            // Assert
            UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent, UserId>(user);

            userCreatedDomainEvent.UserId.Should().Be(user.Id);
        }

        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            // Act
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

            // Assert
            user.Roles.Should().Contain(Role.Registered);
        }

        [Fact]
        public void Create_Should_Fail_WhenUserIsUnderage()
        {
            // Arrange
            var underageBirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(UserData.MinimumAge - 1));

            // Act
            Result<User> result = User.Create(UserData.FirstName, UserData.LastName, underageBirthDate, UserData.Email, UserData.PhoneNumber);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.Underage);
        }

        [Fact]
        public void SetCity_Should_SetCityAndCityId()
        {
            // Arrange
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

            // Act
            user.SetCity(UserData.City);

            // Assert
            user.City.Should().Be(UserData.City);
            user.CityId.Should().Be(UserData.City.Id);
        }

        [Fact]
        public void SetCity_Should_ThrowException_WhenCityIsNull()
        {
            // Arrange
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

            // Act
            Action setNullCity = () => user.SetCity(null!);

            // Assert
            setNullCity.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetIdentityId_Should_SetIdentityId()
        {
            // Arrange
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;
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
            User user = User.Create(UserData.FirstName, UserData.LastName, UserData.BirthDate, UserData.Email, UserData.PhoneNumber).Value;

            // Act
            Action setNullIdentityId = () => user.SetIdentityId(null!);

            // Assert
            setNullIdentityId.Should().Throw<ArgumentNullException>();
        }
    }
}
