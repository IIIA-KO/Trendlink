using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.GetLoggedInUserCooperations
{
    public sealed class CooperationResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public DateTimeOffset ScheduledOnUtc { get; init; }

        public Guid AdvertisementId { get; init; }

        public Guid BuyerId { get; init; }

        public Guid SellerId { get; init; }

        public CooperationStatus Status { get; init; }
    }
}
