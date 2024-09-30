using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;

namespace Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage
{
    public class LocationRatio
    {
        public List<LocationPercentage> TopLocationPercentages { get; }

        public LocationRatio(List<LocationPercentage> locationPercentages)
        {
            var sortedLocations = locationPercentages.OrderByDescending(l => l.Percentage).ToList();

            var topLocations = sortedLocations.Take(4).ToList();

            double otherPercentage = sortedLocations.Skip(4).Sum(l => l.Percentage);

            if (otherPercentage > 0)
            {
                topLocations.Add(
                    new LocationPercentage { Location = "Other", Percentage = otherPercentage }
                );
            }

            this.TopLocationPercentages = topLocations;
        }
    }
}
