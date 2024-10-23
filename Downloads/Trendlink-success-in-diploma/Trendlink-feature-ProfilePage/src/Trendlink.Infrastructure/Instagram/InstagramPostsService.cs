using System.Globalization;
using Microsoft.Extensions.Options;
using Trendlink.Application.Instagarm.Posts.GetPosts;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Posts;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramPostsService : InstagramBaseService, IInstagramPostsService
    {
        public InstagramPostsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
            : base(httpClient, instagramOptions) { }

        public async Task<Result<PostsResponse>> GetUserPostsWithInsights(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        )
        {
            InstagramMedia? posts = await this.FetchPostsAsync(
                instagramAccountId,
                accessToken,
                limit,
                cursorType,
                cursor,
                cancellationToken
            );

            if (posts is null || posts.Data is null)
            {
                return new PostsResponse { Posts = [] };
            }

            await this.FetchInsightsForPostsAsync(posts.Data, accessToken, cancellationToken);

            return this.MapToUserPostsResponse(posts);
        }

        private async Task<InstagramMedia?> FetchPostsAsync(
            string instagramAccountId,
            string accessToken,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "fields", "id,media_type,media_url,permalink,thumbnail_url,timestamp" },
                { "limit", limit.ToString(CultureInfo.InvariantCulture) },
                { "access_token", accessToken }
            };

            if (!string.IsNullOrEmpty(cursor))
            {
                parameters[cursorType] = cursor;
            }

            string url = this.BuildUrl($"{instagramAccountId}/media", parameters);

            return await this.GetAsync<InstagramMedia>(url, cancellationToken);
        }

        private async Task FetchInsightsForPostsAsync(
            List<InstagramPostResponse> posts,
            string accessToken,
            CancellationToken cancellationToken
        )
        {
            IEnumerable<Task> fetchInsightsTasks = posts.Select(post =>
                Task.Run(
                    async () =>
                        post.Insights = await this.FetchPostInsightsAsync(
                            post,
                            accessToken,
                            post.MediaType,
                            cancellationToken
                        )
                )
            );

            await Task.WhenAll(fetchInsightsTasks);
        }

        private async Task<InstagramInsightsResponse?> FetchPostInsightsAsync(
            InstagramPostResponse post,
            string accessToken,
            string mediaType,
            CancellationToken cancellationToken
        )
        {
            string metrics = mediaType.Equals("video", StringComparison.OrdinalIgnoreCase)
                ? "likes,saved,video_views"
                : "likes,saved";

            var parameters = new Dictionary<string, string>
            {
                { "metric", metrics },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl($"{post.Id}/insights", parameters);

            InstagramInsightsResponse? insights = await this.GetAsync<InstagramInsightsResponse>(
                url,
                cancellationToken
            );

            if (mediaType.Equals("video", StringComparison.OrdinalIgnoreCase) || insights is null)
            {
                return insights;
            }

            RemoveVideoViewsInsight(insights);
            return insights;
        }

        private static void RemoveVideoViewsInsight(InstagramInsightsResponse insights)
        {
            InstagramInsightResponse? videoViewsInsight = insights.Data.Find(insight =>
                insight.Name == "video_views"
            );

            videoViewsInsight?.Values.ForEach(v => v.Value = null);
        }

        private PostsResponse MapToUserPostsResponse(InstagramMedia posts)
        {
            return new PostsResponse
            {
                Posts = posts.Data.ConvertAll(post => new InstagramPost
                {
                    Id = post.Id,
                    MediaType = post.MediaType,
                    MediaUrl = post.MediaUrl,
                    Permalink = post.Permalink,
                    ThumbnailUrl = post.ThumbnailUrl,
                    Timestamp = DateTime.Parse(post.Timestamp, CultureInfo.InvariantCulture),
                    Insights =
                        post.Insights?.Data?.ConvertAll(insight => new InstagramInsight
                        {
                            Name = insight.Name,
                            Value = insight.Values.FirstOrDefault()?.Value
                        }) ?? []
                }),
                Paging = new InstagramPagingResponse
                {
                    After = posts.Paging.Cursors.After,
                    Before = posts.Paging.Cursors.Before,
                    NextCursor = posts.Paging.Next,
                    PreviousCursor = posts.Paging.Previous
                }
            };
        }
    }
}
