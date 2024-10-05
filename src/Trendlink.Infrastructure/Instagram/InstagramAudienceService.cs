using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachRatio;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Abstraction;
using static System.Text.Json.JsonElement;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramAudienceService : InstagramBaseService, IInstagramAudienceService
    {
        public InstagramAudienceService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
            : base(httpClient, instagramOptions) { }

        public async Task<Result<GenderRatio>> GetAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "follower_demographics" },
                { "period", "lifetime" },
                { "metric_type", "total_value" },
                { "breakdown", "gender" },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl($"{instagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseGenderRatio(response)
                : Result.Failure<GenderRatio>(Error.Unexpected);
        }

        public async Task<Result<LocationRatio>> GetAudienceTopLocations(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "follower_demographics" },
                { "period", "lifetime" },
                { "metric_type", "total_value" },
                { "breakdown", locationType == LocationType.City ? "city" : "country" },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl($"{instagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseLocationRatio(response)
                : Result.Failure<LocationRatio>(Error.Unexpected);
        }

        public async Task<Result<AgeRatio>> GetAudienceAgesPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "follower_demographics" },
                { "period", "lifetime" },
                { "metric_type", "total_value" },
                { "breakdown", "age" },
                { "access_token", accessToken }
            };

            string url = this.BuildUrl($"{instagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseAgeRatio(response)
                : Result.Failure<AgeRatio>(Error.Unexpected);
        }

        public async Task<Result<ReachRatio>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "reach" },
                { "period", "day" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "breakdown", "follow_type" },
                { "metric_type", "total_value" },
                { "access_token", request.AccessToken }
            };

            string url = this.BuildUrl($"{request.InstagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseReachRatio(response)
                : Result.Failure<ReachRatio>(Error.Unexpected);
        }

        private static Result<GenderRatio> ParseGenderRatio(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<GenderRatio>(Error.NoData);
            }

            List<GenderPercentage> genderPercentages =
                ParseGenderDemographicBreakdownWithPercentage(response);

            return new GenderRatio(genderPercentages);
        }

        private static Result<AgeRatio> ParseAgeRatio(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<AgeRatio>(Error.NoData);
            }

            List<AgePercentage> agePercentages = ParseAgeDemographicBreakdownWithPercentage(
                response
            );
            return new AgeRatio(agePercentages);
        }

        private static Result<LocationRatio> ParseLocationRatio(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<LocationRatio>(Error.NoData);
            }

            List<LocationPercentage> locationPercentages =
                ParseLocationDemographicBreakdownWithPercentage(response);

            var sortedLocations = locationPercentages.OrderByDescending(l => l.Percentage).ToList();

            var topLocations = sortedLocations.Take(4).ToList();

            double otherPercentage = sortedLocations.Skip(4).Sum(l => l.Percentage);

            if (otherPercentage > 0)
            {
                topLocations.Add(
                    new LocationPercentage { Location = "Other", Percentage = otherPercentage }
                );
            }

            return new LocationRatio(topLocations);
        }

        private static Result<ReachRatio> ParseReachRatio(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<ReachRatio>(Error.NoData);
            }

            List<ReachPercentage> reachPercentages = ParseReachDemographicBreakdownWithPercentage(
                response
            );
            int totalReach = response
                .GetProperty("data")[0]
                .GetProperty("total_value")
                .GetProperty("breakdowns")[0]
                .GetProperty("results")
                .EnumerateArray()
                .Sum(x => x.GetProperty("value").GetInt32());

            return new ReachRatio(totalReach, reachPercentages);
        }

        private static List<T> ParseDemographicBreakdownWithPercentage<T>(
            JsonElement response,
            Func<string, double, T> createInstance
        )
        {
            ArrayEnumerator results = response
                .GetProperty("data")[0]
                .GetProperty("total_value")
                .GetProperty("breakdowns")[0]
                .GetProperty("results")
                .EnumerateArray();

            double totalValue = results.Sum(result => result.GetProperty("value").GetDouble());

            var percentages = new List<T>();

            foreach (JsonElement result in results)
            {
                string dimensionValue = result.GetProperty("dimension_values")[0].GetString();
                double value = result.GetProperty("value").GetDouble();
                double percentage = value / totalValue * 100;

                percentages.Add(createInstance(dimensionValue!, percentage));
            }

            return percentages;
        }

        private static List<LocationPercentage> ParseLocationDemographicBreakdownWithPercentage(
            JsonElement response
        )
        {
            return ParseDemographicBreakdownWithPercentage(
                response,
                (location, percentage) =>
                    new LocationPercentage { Location = location, Percentage = percentage }
            );
        }

        private static List<AgePercentage> ParseAgeDemographicBreakdownWithPercentage(
            JsonElement response
        )
        {
            return ParseDemographicBreakdownWithPercentage(
                response,
                (ageGroup, percentage) =>
                    new AgePercentage { AgeGroup = ageGroup, Percentage = percentage }
            );
        }

        private static List<GenderPercentage> ParseGenderDemographicBreakdownWithPercentage(
            JsonElement response
        )
        {
            return ParseDemographicBreakdownWithPercentage(
                response,
                (gender, percentage) =>
                    new GenderPercentage { Gender = gender, Percentage = percentage }
            );
        }

        private static List<ReachPercentage> ParseReachDemographicBreakdownWithPercentage(
            JsonElement response
        )
        {
            return ParseDemographicBreakdownWithPercentage(
                response,
                (followType, percentage) =>
                    new ReachPercentage { FollowType = followType, Percentage = percentage }
            );
        }
    }
}
