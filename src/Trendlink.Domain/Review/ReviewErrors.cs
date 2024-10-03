using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Review
{
    public static class ReviewErrors
    {
        public static readonly Error NotEligible =
            new(
                "Review.NotEligible",
                "The review is not eligible because the cooperation is not yet completed"
            );

        public static readonly Error InvalidComment =
            new("Review.InvalidComment", "The provided comment is invalid");

        public static readonly NotFoundError NotFound = new NotFoundError(
            "Review.NotFound",
            "The review with the specified identifier was not found"
        );
    }
}
