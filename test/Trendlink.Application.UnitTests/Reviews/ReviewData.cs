using Trendlink.Application.UnitTests.Cooperations;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Reviews
{
    internal static class ReviewData
    {
        public static Review Create() =>
            Review.Create(Cooperation, Rating, Comment, DateTime.Now).Value;

        public static readonly Cooperation Cooperation =
            CooperationData.CreateCompletedCooperation();

        public static readonly UserId BuyerId = UserId.New();

        public static readonly UserId SellerId = UserId.New();

        public static readonly Rating Rating = Rating.Create(5).Value;

        public static readonly Comment Comment = new Comment("Test comment");

        public static readonly DateTime UtcNow = DateTime.UtcNow;
    }
}
