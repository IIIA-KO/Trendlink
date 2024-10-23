using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.GetUserReviews
{
    public sealed record GetUserReviewsQuery(
        UserId UserId,
        string? SearchTerm,
        int? Rating,
        int PageNumber,
        int PageSize
    ) : IQuery<PagedList<ReviewResponse>>;
}
