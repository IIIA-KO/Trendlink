using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Review;

namespace Trendlink.Application.Reviews.GetReview
{
    public sealed record GetReviewQuery(ReviewId ReviewId) : IQuery<ReviewResponse>;
}
