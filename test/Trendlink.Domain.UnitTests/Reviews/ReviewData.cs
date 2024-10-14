using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.UnitTests.Cooperations;

namespace Trendlink.Domain.UnitTests.Reviews
{
    internal static class ReviewData
    {
        public static readonly Rating ValidRating = Rating.Create(4).Value;

        public static readonly Comment ValidComment = new("This is a valid comment");
        public static readonly Comment InvalidComment = new(string.Empty);

        public static readonly Cooperation CompletedCooperation =
            CooperationData.CreateCompletedCooperation();

        public static readonly Cooperation NotCompletedCooperation =
            CooperationData.CreatePendingCooperation();

        public static readonly DateTime CreatedOnUtc = DateTime.UtcNow;
    }
}
