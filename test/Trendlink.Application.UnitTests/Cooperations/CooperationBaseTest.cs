using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public abstract class CooperationBaseTest
    {
        public Cooperation CreatePendingCooperation()
        {
            return Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    DateTimeOffset.UtcNow.AddDays(7),
                    AdvertisementData.Create(),
                    UserId.New(),
                    UserId.New(),
                    DateTime.UtcNow
                )
                .Value;
        }

        public Cooperation CreateConfirmedCooperation()
        {
            Cooperation cooperation = this.CreatePendingCooperation();
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }
    }
}
