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
            DateTimeOffset dateTime,
            AdvertisementId advertisementId,
            UserId buyerId,
            UserId selllerId
        )
            : base(id)
        {
            this.Name = name;
            this.Description = description;
            this.ScheduledOnUtc = dateTime;
            this.AdvertisementId = advertisementId;
            this.BuyerId = buyerId;
            this.SellerId = selllerId;
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

        public static Cooperation Pend(
            Name name,
            Description description,
            DateTimeOffset dateTime,
            AdvertisementId advertisementId,
            UserId buyerId,
            UserId selllerId
        )
        {
            var cooperation = new Cooperation(
                CooperationId.New(),
                name,
                description,
                dateTime,
                advertisementId,
                buyerId,
                selllerId
            )
            {
                Status = CooperationStatus.Pending
            };

            cooperation.RaiseDomainEvent(new CooperationPendedDomainEvent(cooperation.Id));

            return cooperation;
        }
    }
}
