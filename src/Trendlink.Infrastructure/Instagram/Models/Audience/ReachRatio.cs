using Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage;

namespace Trendlink.Infrastructure.Instagram.Models.Audience
{
    internal sealed class ReachRatio
    {
        public int TotalReach { get; }

        public List<ReachPercentage> ReachPercentages { get; }

        public ReachRatio(Dictionary<string, int> followsCounts, int totalFollowers)
        {
            this.TotalReach = totalFollowers;

            this.ReachPercentages = followsCounts
                .Select(g => new ReachPercentage
                {
                    FollowType = g.Key switch
                    {
                        "FOLLOWER" => "Follower",
                        _ => "NonFollower"
                    },
                    Percentage = (double)g.Value / totalFollowers * 100
                })
                .ToList();
        }
    }
}
