using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Review;

namespace Trendlink.Application.Reviews.EditReview
{
    public sealed record EditReviewCommand(ReviewId ReviewId, int Rating, Comment Comment)
        : ICommand;
}
