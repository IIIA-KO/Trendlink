using Trendlink.Application.Users.Instagarm.Posts.GetPostsTableStatistics;
using Trendlink.Application.Users.Instagarm.Posts.GetUserPosts;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramPostsService
    {
        Task<Result<UserPostsResponse>> GetUserPostsWithInsights(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );

        Task<Result<PostsTableStatistics>> GetPostsTableStatistics(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        );
    }
}
