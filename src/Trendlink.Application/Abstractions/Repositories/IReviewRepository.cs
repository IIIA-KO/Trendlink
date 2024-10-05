using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(ReviewId id, CancellationToken cancellationToken = default);

        Task<Review?> GetByCooperationIdAndBuyerIdAsync(
            CooperationId cooperationId,
            UserId buyerId,
            CancellationToken cancellationToken = default
        );

        void Add(Review review);

        void Remove(Review review);

        IQueryable<Review> SearchReviews(ReviewSearchParameters parameters, UserId sellerId);
    }

    public sealed record ReviewSearchParameters(string? SearchTerm, int? Rating);
}
