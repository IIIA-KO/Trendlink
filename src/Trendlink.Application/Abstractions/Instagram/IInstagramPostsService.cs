using Trendlink.Application.Users.Instagarm.GetUserPosts;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramPostsService
    {
        Task<UserPostsResponse> GetUserPostsWithInsights(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );
    }
}
