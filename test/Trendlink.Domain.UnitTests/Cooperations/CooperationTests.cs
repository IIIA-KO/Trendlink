using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.UnitTests.Advertisements;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.UnitTests.Cooperations
{
    public class CooperationTests : BaseTest
    {
        [Fact]
        public void Pend_Should_SetPropertyValues()
        {
            // Arrange
            Advertisement advertisement = AdvertisementData.Create();
            var buyerId = UserId.New();
            var sellerId = UserId.New();
            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Cooperation cooperation = Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    scheduledOnUtc,
                    AdvertisementData.Price,
                    advertisement,
                    buyerId,
                    sellerId,
                    utcNow
                )
                .Value;

            // Assert
            cooperation.Name.Should().Be(CooperationData.Name);
            cooperation.Description.Should().Be(CooperationData.Description);
            cooperation.ScheduledOnUtc.Should().Be(scheduledOnUtc);
            cooperation.AdvertisementId.Should().Be(advertisement.Id);
            cooperation.Advertisement.Should().BeNull();
            cooperation.BuyerId.Should().Be(buyerId);
            cooperation.Buyer.Should().BeNull();
            cooperation.SellerId.Should().Be(sellerId);
            cooperation.Seller.Should().BeNull();
            cooperation.PendedOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void Pend_Should_RaiseCooperationPendedDomainEvent()
        {
            // Arrange
            Advertisement advertisement = AdvertisementData.Create();
            var buyerId = UserId.New();
            var sellerId = UserId.New();
            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Cooperation cooperation = Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    scheduledOnUtc,
                    AdvertisementData.Price,
                    advertisement,
                    buyerId,
                    sellerId,
                    utcNow
                )
                .Value;

            // Assert
            CooperationPendedDomainEvent cooperationPendedDomainEvent =
                AssertDomainEventWasPublished<CooperationPendedDomainEvent, CooperationId>(
                    cooperation
                );

            cooperationPendedDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void Pend_Should_ReturnError_WhenBuyerAndSellerAreSame()
        {
            // Arrange
            Advertisement advertisement = AdvertisementData.Create();
            var userId = UserId.New();
            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result<Cooperation> result = Cooperation.Pend(
                CooperationData.Name,
                CooperationData.Description,
                scheduledOnUtc,
                AdvertisementData.Price,
                advertisement,
                userId,
                userId,
                utcNow
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.SameUser);
        }

        [Fact]
        public void Pend_Should_ReturnFailure_WhenCooperationScheduledOnPastTime()
        {
            Advertisement advertisement = AdvertisementData.Create();
            var buyerId = UserId.New();
            var sellerId = UserId.New();
            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(-7);
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result<Cooperation> result = Cooperation.Pend(
                CooperationData.Name,
                CooperationData.Description,
                scheduledOnUtc,
                AdvertisementData.Price,
                advertisement,
                buyerId,
                sellerId,
                utcNow
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.InvalidTime);
        }

        [Fact]
        public void Pend_Should_ReturnFailure_WhenPriceIsInvalid()
        {
            // Arrange
            Advertisement advertisement = AdvertisementData.Create();
            var buyerId = UserId.New();
            var sellerId = UserId.New();
            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result<Cooperation> result = Cooperation.Pend(
                CooperationData.Name,
                CooperationData.Description,
                scheduledOnUtc,
                new Money(-1, Currency.FromCode("USD")),
                advertisement,
                buyerId,
                sellerId,
                utcNow
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.InvalidPrice);
        }

        [Fact]
        public void Confirm_Should_SetStatusAndConfirmedOnUtc()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Confirm(utcNow);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cooperation.Status.Should().Be(CooperationStatus.Confirmed);
            cooperation.ConfirmedOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void Confirm_Should_RaiseCooperationConfirmedDomainEvent()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            cooperation.Confirm(utcNow);

            // Assert
            CooperationConfirmedDomainEvent cooperationConfirmedDomainEvent =
                AssertDomainEventWasPublished<CooperationConfirmedDomainEvent, CooperationId>(
                    cooperation
                );

            cooperationConfirmedDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void Confirm_Should_ReturnFailure_WhenStatusIsNotPending()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Confirm(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotPending);
        }

        [Fact]
        public void Reject_Should_SetStatusAndRejectedOnUtc()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Reject(utcNow);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cooperation.Status.Should().Be(CooperationStatus.Rejected);
            cooperation.RejectedOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void Reject_Should_RaiseCooperationRejectedDomainEvent()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            cooperation.Reject(utcNow);

            // Assert
            CooperationRejectedDomainEvent cooperationRejectedDomainEvent =
                AssertDomainEventWasPublished<CooperationRejectedDomainEvent, CooperationId>(
                    cooperation
                );

            cooperationRejectedDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void Reject_Should_ReturnError_WhenStatusIsNotPending()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Reject(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotPending);
        }

        [Fact]
        public void MarkAsDone_Should_SetStatusAndDoneOnUtc()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.MarkAsDone(utcNow);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cooperation.Status.Should().Be(CooperationStatus.Done);
            cooperation.DoneOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void MarkAsDone_Should_RaiseCooperationDoneDomainEvent()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            cooperation.MarkAsDone(utcNow);

            // Assert
            CooperationDoneDomainEvent cooperationDoneDomainEvent = AssertDomainEventWasPublished<
                CooperationDoneDomainEvent,
                CooperationId
            >(cooperation);

            cooperationDoneDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void MarkAsDone_Should_ReturnFailure_WhenStatusIsNotConfirmed()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.MarkAsDone(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotConfirmed);
        }

        [Fact]
        public void Complete_Should_SetStatusAndCompletedOnUtc()
        {
            // Arrange
            Cooperation cooperation = CreateDoneCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Complete(utcNow);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cooperation.Status.Should().Be(CooperationStatus.Completed);
            cooperation.CompletedOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void Complete_Should_RaiseCooperationCompletedDomainEvent()
        {
            // Arrange
            Cooperation cooperation = CreateDoneCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            cooperation.Complete(utcNow);

            // Assert
            CooperationCompletedDomainEvent cooperationCompletedDomainEvent =
                AssertDomainEventWasPublished<CooperationCompletedDomainEvent, CooperationId>(
                    cooperation
                );

            cooperationCompletedDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void Complete_Should_ReturnFailure_WhenStatusIsNotDone()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Complete(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotDone);
        }

        [Fact]
        public void Cancel_Should_SetStatusAndCancelledOnUtc()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow.AddDays(-1);

            // Act
            Result result = cooperation.Cancel(utcNow);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cooperation.Status.Should().Be(CooperationStatus.Cancelled);
            cooperation.CancelledOnUtc.Should().Be(utcNow);
        }

        [Fact]
        public void Cancel_Should_RaiseCooperationCancelledDomainEvent()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow.AddDays(-1);

            // Act
            cooperation.Cancel(utcNow);

            // Assert
            CooperationCancelledDomainEvent cooperationCancelledDomainEvent =
                AssertDomainEventWasPublished<CooperationCancelledDomainEvent, CooperationId>(
                    cooperation
                );

            cooperationCancelledDomainEvent.CooperationId.Should().Be(cooperation.Id);
        }

        [Fact]
        public void Cancel_Should_ReturnFailure_WhenStatusIsNotConfirmed()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation();
            DateTime utcNow = DateTime.UtcNow;

            // Act
            Result result = cooperation.Cancel(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotConfirmed);
        }

        [Fact]
        public void Cancel_Should_ReturnFailure_WhenAlreadyStarted()
        {
            // Arrange
            Cooperation cooperation = CreateConfirmedCooperation();
            DateTime utcNow = DateTime.UtcNow.AddDays(8);

            // Act
            Result result = cooperation.Cancel(utcNow);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.AlreadyStarted);
        }

        private static Cooperation CreatePendingCooperation()
        {
            return Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    DateTimeOffset.UtcNow.AddDays(7),
                    AdvertisementData.Price,
                    AdvertisementData.Create(),
                    UserId.New(),
                    UserId.New(),
                    DateTime.UtcNow
                )
                .Value;
        }

        private static Cooperation CreateConfirmedCooperation()
        {
            Cooperation cooperation = CreatePendingCooperation();
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }

        private static Cooperation CreateDoneCooperation()
        {
            Cooperation cooperation = CreateConfirmedCooperation();
            cooperation.MarkAsDone(DateTime.UtcNow);
            return cooperation;
        }
    }
}
