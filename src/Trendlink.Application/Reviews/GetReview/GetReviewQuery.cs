using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Reviews;

namespace Trendlink.Application.Reviews.GetReview
{
    public sealed record GetReviewQuery(ReviewId ReviewId) : IQuery<ReviewResponse>;
}
