using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramStatiscticsService : IInstagramStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramStatiscticsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
        }

        public async Task<Result<TableStatistics>> GetTableStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{request.InstagramAccountId}/insights?metric=reach,follower_count,impressions,profile_views"
                + "&period=day"
                + $"&since={request.Since}&until={request.Until}"
                + $"&access_token={request.AccessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                var postsTableStatistics = new TableStatistics();

                foreach (
                    JsonElement metricElement in jsonObject.GetProperty("data").EnumerateArray()
                )
                {
                    var metricData = new TimeSeriesMetricData
                    {
                        Name = metricElement.GetProperty("name").GetString()!
                    };

                    foreach (
                        JsonElement valueElement in metricElement
                            .GetProperty("values")
                            .EnumerateArray()
                    )
                    {
                        int metricValue = valueElement.GetProperty("value").GetInt32();

                        string endTimeString = valueElement.GetProperty("end_time").GetString()!;

                        DateTime endTime = DateTimeOffset
                            .ParseExact(
                                endTimeString,
                                "yyyy-MM-ddTHH:mm:sszzz",
                                CultureInfo.InvariantCulture
                            )
                            .UtcDateTime;

                        metricData.Values.Add(endTime, metricValue);
                    }

                    postsTableStatistics.Metrics.Add(metricData);
                }

                return postsTableStatistics;
            }
            catch (Exception)
            {
                return Result.Failure<TableStatistics>(Error.Unexpected);
            }
        }

        public async Task<Result<OverviewStatistics>> GetOverviewStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{request.InstagramAccountId}/insights?metric=reach,impressions,total_interactions,comments,profile_views,website_clicks"
                + "&metric_type=total_value&period=day"
                + $"&since={request.Since}&until={request.Until}"
                + $"&access_token={request.AccessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                var overviewStatistics = new OverviewStatistics();

                foreach (
                    JsonElement metricElement in jsonObject.GetProperty("data").EnumerateArray()
                )
                {
                    var metricData = new TotalValueMetricData
                    {
                        Name = metricElement.GetProperty("name").GetString()!,
                        Value = metricElement
                            .GetProperty("total_value")
                            .GetProperty("value")
                            .GetInt32()
                    };

                    overviewStatistics.Metrics.Add(metricData);
                }

                return overviewStatistics;
            }
            catch (Exception)
            {
                return Result.Failure<OverviewStatistics>(Error.Unexpected);
            }
        }

        public async Task<Result<InteractionStatistics>> GetInteractionsStatistics(
            string instagramAdAccountId,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{request.InstagramAccountId}/insights?metric=total_interactions,reach,likes,comments&period=day"
                + $"&since={request.Since}&until={request.Until}"
                + "&metric_type=total_value"
                + $"&access_token={request.AccessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                int interactions = jsonObject
                    .GetProperty("data")
                    .EnumerateArray()
                    .First(e => e.GetProperty("name").GetString() == "total_interactions")
                    .GetProperty("total_value")
                    .GetProperty("value")
                    .GetInt32();

                int reach = jsonObject
                    .GetProperty("data")
                    .EnumerateArray()
                    .First(e => e.GetProperty("name").GetString() == "reach")
                    .GetProperty("total_value")
                    .GetProperty("value")
                    .GetInt32();

                int totalLikes = jsonObject
                    .GetProperty("data")
                    .EnumerateArray()
                    .First(e => e.GetProperty("name").GetString() == "likes")
                    .GetProperty("total_value")
                    .GetProperty("value")
                    .GetInt32();

                int totalComments = jsonObject
                    .GetProperty("data")
                    .EnumerateArray()
                    .First(e => e.GetProperty("name").GetString() == "comments")
                    .GetProperty("total_value")
                    .GetProperty("value")
                    .GetInt32();

                double engagementRate = (double)interactions / reach * 100;

                int daysCount = request.Until.DayNumber - request.Since.DayNumber + 1;
                double averageLikes = (double)totalLikes / daysCount;
                double averageComments = (double)totalComments / daysCount;

                string adSpendUrl =
                    $"{this._instagramOptions.BaseUrl}{instagramAdAccountId}/insights?fields=spend"
                    + $"&since={request.Since}&until={request.Until}"
                    + $"&access_token={request.AccessToken}";

                HttpResponseMessage adSpendResponse = await this.SendGetRequestAsync(
                    adSpendUrl,
                    cancellationToken
                );
                string adSpendContent = await adSpendResponse.Content.ReadAsStringAsync(
                    cancellationToken
                );
                JsonElement adSpendJson = JsonDocument.Parse(adSpendContent).RootElement;

                double totalAdSpend = 0;
                if (adSpendJson.GetProperty("data").EnumerateArray().Any())
                {
                    totalAdSpend = double.Parse(
                        adSpendJson.GetProperty("data")[0].GetProperty("spend").GetString()!,
                        CultureInfo.InvariantCulture
                    );
                }
                double cpe = 0;
                if (interactions > 0)
                {
                    cpe = totalAdSpend / interactions;
                }

                return new InteractionStatistics(
                    engagementRate,
                    averageLikes,
                    averageComments,
                    cpe
                );
            }
            catch (Exception)
            {
                return Result.Failure<InteractionStatistics>(Error.Unexpected);
            }
        }

        public async Task<Result<EngagementStatistics>> GetEngagementStatistics(
            int followersCount,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            string insightsUrl =
                $"{this._instagramOptions.BaseUrl}{request.InstagramAccountId}/insights?metric=reach,impressions,profile_views,likes,comments,saves"
                + $"&period=day"
                + $"&metric_type=total_value"
                + $"&since={request.Since}&until={request.Until}"
                + $"&access_token={request.AccessToken}";

            HttpResponseMessage insightsResponse = await this.SendGetRequestAsync(
                insightsUrl,
                cancellationToken
            );

            string insightsContent = await insightsResponse.Content.ReadAsStringAsync(
                cancellationToken
            );
            JsonElement insightsJson = JsonDocument.Parse(insightsContent).RootElement;

            if (!insightsJson.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<EngagementStatistics>(Error.NoData);
            }

            double reach = insightsJson
                .GetProperty("data")
                .EnumerateArray()
                .First(e => e.GetProperty("name").GetString() == "reach")
                .GetProperty("total_value")
                .GetProperty("value")
                .GetInt32();

            double likes = insightsJson
                .GetProperty("data")
                .EnumerateArray()
                .First(e => e.GetProperty("name").GetString() == "likes")
                .GetProperty("total_value")
                .GetProperty("value")
                .GetInt32();

            double comments = insightsJson
                .GetProperty("data")
                .EnumerateArray()
                .First(e => e.GetProperty("name").GetString() == "comments")
                .GetProperty("total_value")
                .GetProperty("value")
                .GetInt32();

            double saves = insightsJson
                .GetProperty("data")
                .EnumerateArray()
                .First(e => e.GetProperty("name").GetString() == "saves")
                .GetProperty("total_value")
                .GetProperty("value")
                .GetInt32();

            double totalEngagements = likes + comments + saves;
            double reachRate = reach / followersCount * 100;
            double engagementRate = totalEngagements / followersCount * 100;
            double erReach = totalEngagements / reach * 100;

            return new EngagementStatistics(reachRate, engagementRate, erReach);
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
