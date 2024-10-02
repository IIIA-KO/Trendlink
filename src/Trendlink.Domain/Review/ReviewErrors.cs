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
    }
}
