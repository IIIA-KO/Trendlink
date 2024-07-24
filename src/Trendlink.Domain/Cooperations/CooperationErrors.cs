using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations
{
    public static class CooperationErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "Cooperation.NotFound",
                "The cooperation with the specified identifier was not found"
            );

        public static readonly Error SameUser =
            new("Cooperation.SameUser", "The buyer and seller cannot be the same person");

        public static readonly Error InvalidTime =
            new("Cooperation.InvalidTime", "Scheduled time cannot be in the past");

        public static readonly Error NotPending =
            new("Cooperation.NotPending", "The cooperation is not pending");

        public static readonly Error NotConfirmed =
            new("Cooperation.NotConfirmed", "The cooperation is not confirmed");

        public static readonly Error NotDone =
            new("Cooperation.NotDone", "The cooperation is not done");

        public static readonly Error AlreadyStarted =
            new("Cooperation.AlreadyStarted", "The cooperation has already started");
    }
}
