using System.Globalization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Posts.GetPosts;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Posts;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramPostsService : IInstagramPostsService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramPostsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
        }

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
            string postsUrl = this.BuildPostsUrl(
                instagramAccountId,
                accessToken,
                limit,
                cursorType,
                cursor
            );
            HttpResponseMessage response = await this.SendGetRequestAsync(
                postsUrl,
                cancellationToken
            );
            string postsContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<InstagramMedia>(postsContent);
        }

        private string BuildPostsUrl(
            string instagramAccountId,
            string accessToken,
            int limit,
            string cursorType,
            string cursor
        )
        {
            string baseUrl = $"{this._instagramOptions.BaseUrl}{instagramAccountId}/media";
            const string fields =
                "?fields=id,media_type,media_url,permalink,thumbnail_url,timestamp";
            string query = $"&limit={limit}&access_token={accessToken}";

            if (!string.IsNullOrEmpty(cursor))
            {
                query += $"&{cursorType}={cursor}";
            }

            return baseUrl + fields + query;
        }

        private async Task FetchInsightsForPostsAsync(
            List<InstagramPostResponse> posts,
            string accessToken,
            CancellationToken cancellationToken
        )
        {
            foreach (InstagramPostResponse post in posts)
            {
                post.Insights = await this.FetchPostInsightsAsync(
                    post,
                    accessToken,
                    post.MediaType,
                    cancellationToken
                );
            }
        }

        private async Task<InstagramInsightsResponse?> FetchPostInsightsAsync(
            InstagramPostResponse post,
            string accessToken,
            string mediaType,
            CancellationToken cancellationToken
        )
        {
            string insightsUrl = BuildInsightsUrl(post.Id, accessToken, post.MediaType);
            HttpResponseMessage response = await this.SendGetRequestAsync(
                insightsUrl,
                cancellationToken
            );
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            InstagramInsightsResponse? insights =
                JsonConvert.DeserializeObject<InstagramInsightsResponse>(content);
            if (mediaType.Equals("video", StringComparison.OrdinalIgnoreCase) || insights is null)
            {
                return insights;
            }

            RemoveVideoViewsInsight(insights);
            return insights;
        }

        private static string BuildInsightsUrl(string postId, string accessToken, string mediaType)
        {
            string metrics = mediaType.Equals("video", StringComparison.OrdinalIgnoreCase)
                ? "likes,saved,video_views"
                : "likes,saved";

            return $"https://graph.facebook.com/v18.0/{postId}/insights?metric={metrics}&access_token={accessToken}";
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

        private async Task<HttpResponseMessage> SendGetRequestAsync(
            string url,
            CancellationToken cancellationToken
        )
        {
            return await this._httpClient.GetAsync(url, cancellationToken);
        }
    }
}
