using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations
{
    internal static class CooperationErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "Cooperation.NotFound",
                "The cooperation with the specified identifier was not found"
            );

        public static readonly Error SameUser =
            new("Cooperation.SameUser", "The buyer and seller cannot be the same person");
    }
}
