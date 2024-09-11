using Trendlink.Application.Users.Instagarm.GetUserPosts;

namespace Trendlink.Infrastructure.Instagram
{
    internal interface IInstagramPostsService
    {
        Task<UserPostsResponse> GetPostsAsync(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );
    }
}
