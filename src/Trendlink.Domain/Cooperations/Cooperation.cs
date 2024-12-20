﻿using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Cooperations
{
    public sealed class Cooperation : Entity<CooperationId>
    {
        public static readonly CooperationStatus[] ActiveCooperationStatuses =
        [
            CooperationStatus.Pending,
            CooperationStatus.Confirmed,
            CooperationStatus.Done,
        ];

        private Cooperation() { }

        private Cooperation(
            CooperationId id,
            Name name,
            Description description,
            DateTimeOffset scheduledOnUtc,
            Money price,
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
            this.Price = price;
            this.AdvertisementId = advertisementId;
            this.BuyerId = buyerId;
            this.SellerId = selllerId;
            this.Status = status;
            this.PendedOnUtc = pendedOnUtc;
        }

        public Name Name { get; init; }

        public Description Description { get; init; }

        public DateTimeOffset ScheduledOnUtc { get; init; }

        public Money Price { get; init; }

        public AdvertisementId AdvertisementId { get; init; }

        public Advertisement Advertisement { get; init; }

        public UserId BuyerId { get; init; }

        public User Buyer { get; init; }

        public UserId SellerId { get; init; }

        public User Seller { get; init; }

        public CooperationStatus Status { get; private set; }

        public DateTime PendedOnUtc { get; init; }

        public DateTime? ConfirmedOnUtc { get; private set; }

        public DateTime? RejectedOnUtc { get; private set; }

        public DateTime? CancelledOnUtc { get; private set; }

        public DateTime? DoneOnUtc { get; private set; }

        public DateTime? CompletedOnUtc { get; private set; }

        public static Result<Cooperation> Pend(
            Name name,
            Description description,
            DateTimeOffset scheduledOnUtc,
            Money price,
            Advertisement advertisement,
            UserId buyerId,
            UserId selllerId,
            DateTime utcNow
        )
        {
            ArgumentNullException.ThrowIfNull(advertisement);
            ArgumentNullException.ThrowIfNull(price);
            
            if (buyerId == selllerId)
            {
                return Result.Failure<Cooperation>(CooperationErrors.SameUser);
            }

            if (scheduledOnUtc <= DateTimeOffset.UtcNow)
            {
                return Result.Failure<Cooperation>(CooperationErrors.InvalidTime);
            }

            if (price.Amount <= 0)
            {
                return Result.Failure<Cooperation>(AdvertisementErrors.InvalidPrice);
            }

            var cooperation = new Cooperation(
                CooperationId.New(),
                name,
                description,
                scheduledOnUtc,
                price,
                advertisement.Id,
                buyerId,
                selllerId,
                CooperationStatus.Pending,
                utcNow
            );

            cooperation.RaiseDomainEvent(new CooperationPendedDomainEvent(cooperation.Id));

            advertisement.LastCooperatedOnUtc = utcNow;

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

        public Result MarkAsDone(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Confirmed)
            {
                return Result.Failure(CooperationErrors.NotConfirmed);
            }

            this.Status = CooperationStatus.Done;
            this.DoneOnUtc = utcNow;

            this.RaiseDomainEvent(new CooperationDoneDomainEvent(this.Id));

            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (this.Status != CooperationStatus.Done)
            {
                return Result.Failure(CooperationErrors.NotDone);
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

            this.RaiseDomainEvent(new CooperationCancelledDomainEvent(this.Id));
            return Result.Success();
        }
    }
}
