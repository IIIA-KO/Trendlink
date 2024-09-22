namespace Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage
{
    public class AudienceReachStatistics
    {
        public List<AudienceReachPercentageResponse> ReachPercentages { get; }

        public AudienceReachStatistics(Dictionary<string, int> followsCount, int totalFollowers)
        {
            this.ReachPercentages = followsCount
                .Select(g => new AudienceReachPercentageResponse
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
