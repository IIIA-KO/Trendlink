using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Review;

namespace Trendlink.Application.Reviews.GetReview
{
    internal sealed class GetReviewQueryHandler : IQueryHandler<GetReviewQuery, ReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewQueryHandler(IReviewRepository reviewRepository)
        {
            this._reviewRepository = reviewRepository;
        }

        public async Task<Result<ReviewResponse>> Handle(
            GetReviewQuery request,
            CancellationToken cancellationToken
        )
        {
            Review? review = await this._reviewRepository.GetByIdAsync(
                request.ReviewId,
                cancellationToken
            );
            if (review is null)
            {
                return Result.Failure<ReviewResponse>(ReviewErrors.NotFound);
            }

            return new ReviewResponse(
                review.Id.Value,
                review.Rating.Value,
                review.Comment.Value,
                review.CreatedOnUtc
            );
        }
    }
}
