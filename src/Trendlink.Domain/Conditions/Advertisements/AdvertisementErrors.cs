using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Conditions.Advertisements
{
    public static class AdvertisementErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "Advertisement.NotFound",
                "The advertisement with the specified identifier was not found"
            );

        public static readonly Error InvalidPrice =
            new("Advertisement.InvalidPrice", "The provided price is invalid");

        public static readonly Error Invalid =
            new("Advertisement.Invalid", "The provided advertisement content is invalid");

        public static readonly Error Duplicate =
            new("Advertisement.Duplicate", "Advertisement with such name already exists");

        public static readonly Error HasActiveCooperations = new Error(
            "Advertisement.HasActiveCooperations",
            "You are not allowed to perform this action while the advertisement has active cooperations"
        );
    }
}
