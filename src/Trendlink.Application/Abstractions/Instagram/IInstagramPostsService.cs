using Trendlink.Application.Instagarm.Posts.GetPosts;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramPostsService
    {
        Task<Result<PostsResponse>> GetUserPostsWithInsights(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );
    }
}
