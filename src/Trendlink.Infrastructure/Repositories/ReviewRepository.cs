using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Review;
using Trendlink.Domain.Users;
using Trendlink.Infrastructure.Specifications.Reviews;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class ReviewRepository : Repository<Review, ReviewId>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<Review?> GetByCooperationIdAndBuyerIdAsync(
            CooperationId cooperationId,
            UserId buyerId,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new ReviewByCooperationIdSpecification(cooperationId)
                        & new ReviewByBuyerIdSpecification(buyerId)
                )
                .FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<Review> SearchReviews(ReviewSearchParameters parameters, UserId sellerId)
        {
            IQueryable<Review> query = this
                .dbContext.Set<Review>()
                .Where(review => review.SellerId == sellerId);

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(review =>
                    ((string)review.Comment).Contains(parameters.SearchTerm)
                );
            }

            if (parameters.Rating is not null)
            {
                query = query.Where(review => review.Rating.Value >= parameters.Rating.Value);
            }

            return query;
        }
    }
}
