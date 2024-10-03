using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Reviews;

namespace Trendlink.Application.Reviews.EditReview
{
    public sealed record EditReviewCommand(ReviewId ReviewId, int Rating, Comment Comment)
        : ICommand;
}
