using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.GetUsers
{
    public sealed record GetUsersQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        AccountCategory? AccountCategory,
        int MinFollowersCount,
        int MinMediaCount,
        int PageNumber,
        int PageSize
    ) : IQuery<PagedList<UserResponse>>;
}
