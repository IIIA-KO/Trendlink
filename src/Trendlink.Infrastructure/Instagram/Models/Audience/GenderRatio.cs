using Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio;

namespace Trendlink.Infrastructure.Instagram.Models.Audience
{
    internal sealed class GenderRatio
    {
        public List<GenderPercentage> GenderPercentages { get; }

        public GenderRatio(Dictionary<string, int> genderCounts, int totalFollowers)
        {
            this.GenderPercentages = genderCounts
                .Select(g => new GenderPercentage
                {
                    Gender = g.Key switch
                    {
                        "F" => "Female",
                        "M" => "Male",
                        _ => "Unknown"
                    },
                    Percentage = (double)g.Value / totalFollowers * 100
                })
                .ToList();
        }
    }
}
