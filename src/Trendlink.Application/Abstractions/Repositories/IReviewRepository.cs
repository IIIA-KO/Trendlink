using Trendlink.Domain.Review;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(ReviewId id, CancellationToken cancellationToken = default);

        void Add(Review review);

        void Remove(Review user);

        Task<int> CountUserReviews(UserId sellerId, CancellationToken cancellationToken = default);
    }
}
