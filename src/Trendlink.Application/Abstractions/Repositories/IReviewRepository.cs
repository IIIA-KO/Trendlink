using Trendlink.Domain.Review;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(ReviewId id, CancellationToken cancellationToken = default);

        void Add(Review review);

        void Remove(Review user);
    }
}
