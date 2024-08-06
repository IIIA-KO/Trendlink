using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public abstract class CooperationBaseTest
    {
        public Cooperation CreatePendingCooperation(DateTimeOffset scheduledOnUtc)
        {
            return Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    scheduledOnUtc,
                    AdvertisementData.Price,
                    AdvertisementData.Create(),
                    UserId.New(),
                    UserId.New(),
                    DateTime.UtcNow
                )
                .Value;
        }

        public Cooperation CreateConfirmedCooperation(DateTimeOffset scheduledOnUtc)
        {
            Cooperation cooperation = this.CreatePendingCooperation(scheduledOnUtc);
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }

        public Cooperation CreateDoneCooperation()
        {
            Cooperation cooperation = this.CreateConfirmedCooperation(
                DateTimeOffset.UtcNow.AddDays(7)
            );
            cooperation.MarkAsDone(DateTime.UtcNow);
            return cooperation;
        }
    }
}
