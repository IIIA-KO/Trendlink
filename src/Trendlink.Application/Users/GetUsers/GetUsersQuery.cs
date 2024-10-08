﻿using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Pagination;

namespace Trendlink.Application.Users.GetUsers
{
    public sealed record GetUsersQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        string? Country,
        string? AccountCategory,
        int MinFollowersCount,
        int MinMediaCount,
        int PageNumber,
        int PageSize
    ) : IQuery<PagedList<UserResponse>>;
}
