using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Review;
using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class ReviewRepository : Repository<Review, ReviewId>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<int> CountUserReviews(
            UserId sellerId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<Review>()
                .Where(review => review.SellerId == sellerId)
                .CountAsync(cancellationToken);
        }
    }
}
