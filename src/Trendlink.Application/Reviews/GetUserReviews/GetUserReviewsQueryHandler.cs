using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.GetUserReviews
{
    internal sealed class GetUserReviewsQueryHandler
        : IQueryHandler<GetUserReviewsQuery, PagedList<ReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;

        public GetUserReviewsQueryHandler(
            IReviewRepository reviewRepository,
            IUserRepository userRepository
        )
        {
            this._reviewRepository = reviewRepository;
            this._userRepository = userRepository;
        }

        public async Task<Result<PagedList<ReviewResponse>>> Handle(
            GetUserReviewsQuery request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.ExistsByIdAsync(
                request.UserId,
                cancellationToken
            );
            if (!userExists)
            {
                return Result.Failure<PagedList<ReviewResponse>>(UserErrors.NotFound);
            }

            IQueryable<Review> reviewsQuery = this._reviewRepository.SearchReviews(
                new ReviewSearchParameters(request.SearchTerm, request.Rating),
                request.UserId
            );

            IQueryable<ReviewResponse> reviewResponsesQuery = reviewsQuery.Select(
                review => new ReviewResponse(
                    review.Id.Value,
                    review.Rating.Value,
                    review.Comment.Value,
                    review.CreatedOnUtc
                )
            );

            return await PagedList<ReviewResponse>.CreateAsync(
                reviewResponsesQuery,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
