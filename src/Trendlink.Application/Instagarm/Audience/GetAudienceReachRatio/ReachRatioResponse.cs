namespace Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage
{
    public class ReachRatioResponse
    {
        public int TotalReach { get; }

        public List<ReachPercentage> ReachPercentages { get; }

        public ReachRatioResponse(int totalReach, List<ReachPercentage> reachPercentages)
        {
            this.TotalReach = totalReach;
            this.ReachPercentages = reachPercentages;
        }
    }
}
