using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public abstract class CooperationBaseTest
    {
        public static Cooperation CreatePendingCooperation(DateTimeOffset scheduledOnUtc)
        {
            return Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    scheduledOnUtc,
                    AdvertisementData.Create(),
                    UserId.New(),
                    UserId.New(),
                    DateTime.UtcNow
                )
                .Value;
        }

        public static Cooperation CreateConfirmedCooperation(DateTimeOffset scheduledOnUtc)
        {
            Cooperation cooperation = CreatePendingCooperation(scheduledOnUtc);
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }

        public static Cooperation CreateDoneCooperation()
        {
            Cooperation cooperation = CreateConfirmedCooperation(DateTimeOffset.UtcNow.AddDays(7));
            cooperation.MarkAsDone(DateTime.UtcNow);
            return cooperation;
        }
    }
}
