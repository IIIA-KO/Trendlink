using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Review;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class ReviewRepository : Repository<Review, ReviewId>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
