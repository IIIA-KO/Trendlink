using Trendlink.Domain.Review;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Specifications.Reviews
{
    internal sealed class ReviewByBuyerIdSpecification : Specification<Review, ReviewId>
    {
        public ReviewByBuyerIdSpecification(UserId userId)
            : base(review => review.BuyerId == userId) { }
    }
}
