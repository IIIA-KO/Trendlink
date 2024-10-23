using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Reviews;

namespace Trendlink.Application.Reviews.DeleteReview
{
    public sealed record DeleteReviewCommand(ReviewId ReviewId) : ICommand;
}
