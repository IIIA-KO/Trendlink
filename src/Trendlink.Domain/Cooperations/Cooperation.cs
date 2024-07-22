using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Cooperations
{
    public sealed class Cooperation : Entity<CooperationId>
    {
        private Cooperation() { }

        private Cooperation(
            CooperationId id,
            Name name,
            Description description,
            DateTimeOffset scheduledOnUtc,
            AdvertisementId advertisementId,
            UserId buyerId,
            UserId selllerId,
            CooperationStatus status,
            DateTime pendedOnUtc
        )
            : base(id)
        {
            this.Name = name;
            this.Description = description;
            this.ScheduledOnUtc = scheduledOnUtc;
            this.AdvertisementId = advertisementId;
            this.BuyerId = buyerId;
            this.SellerId = selllerId;
            this.Status = status;
            this.PendedOnUtc = pendedOnUtc;
        }

        public Name Name { get; private set; }

        public Description Description { get; private set; }

        public DateTimeOffset ScheduledOnUtc { get; private set; }

        public AdvertisementId AdvertisementId { get; private set; }

        public Advertisement Advertisement { get; init; }

        public UserId BuyerId { get; private set; }

        public User Buyer { get; init; }

        public UserId SellerId { get; private set; }

        public User Seller { get; init; }

        public CooperationStatus Status { get; set; }

        public DateTime PendedOnUtc { get; private set; }

        public DateTime? ConfirmedOnUtc { get; private set; }

        public DateTime? RejectedOnUtc { get; private set; }

        public DateTime? CancelledOnUtc { get; private set; }

        public DateTime? CompletedOnUtc { get; private set; }

        public static Result<Cooperation> Pend(
            Name name,
            Description description,
            DateTimeOffset scheduledOnUtc,
            AdvertisementId advertisementId,
            UserId buyerId,
            UserId selllerId,
            DateTime utcNow
        )
        {
            if (buyerId == selllerId)
            {
                return Result.Failure<Cooperation>(CooperationErrors.SameUser);
            }

            var cooperation = new Cooperation(
                CooperationId.New(),
                name,
                description,
                scheduledOnUtc,
                advertisementId,
                buyerId,
                selllerId,
                CooperationStatus.Pending,
                utcNow
            );

            cooperation.RaiseDomainEvent(new CooperationPendedDomainEvent(cooperation.Id));

            return cooperation;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Pending)
            {
                return Result.Failure(CooperationErrors.NotPending);
            }

            this.Status = CooperationStatus.Confirmed;
            this.ConfirmedOnUtc = utcNow;

            this.RaiseDomainEvent(new CooperationConfirmedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Pending)
            {
                return Result.Failure(CooperationErrors.NotPending);
            }

            this.Status = CooperationStatus.Rejected;
            this.RejectedOnUtc = utcNow;

            this.RaiseDomainEvent(new CooperationRejectedDomainEvent(this.Id));
            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Confirmed)
            {
                return Result.Failure(CooperationErrors.NotConfirmed);
            }

            this.Status = CooperationStatus.Completed;
            this.CompletedOnUtc = utcNow;

            this.RaiseDomainEvent(new CooperationCompletedDomainEvent(this.Id));
            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Confirmed)
            {
                return Result.Failure(CooperationErrors.NotConfirmed);
            }

            if (utcNow > this.ScheduledOnUtc)
            {
                return Result.Failure(CooperationErrors.AlreadyStarted);
            }

            this.Status = CooperationStatus.Cancelled;
            this.CancelledOnUtc = utcNow;

            this.RaiseDomainEvent(new CooperationsCancelledDomainEvent(this.Id));
            return Result.Success();
        }
    }
}
