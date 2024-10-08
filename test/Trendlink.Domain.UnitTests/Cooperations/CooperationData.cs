using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Shared;
using Trendlink.Domain.UnitTests.Advertisements;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.UnitTests.Cooperations
{
    internal static class CooperationData
    {
        public static readonly Name Name = new("Name");

        public static readonly Description Description = new("Description");

        public static Cooperation CreatePendingCooperation()
        {
            return Cooperation
                .Pend(
                    CooperationData.Name,
                    CooperationData.Description,
                    DateTimeOffset.UtcNow.AddDays(7),
                    AdvertisementData.Price,
                    AdvertisementData.Create(),
                    UserId.New(),
                    UserId.New(),
                    DateTime.UtcNow
                )
                .Value;
        }

        public static Cooperation CreateConfirmedCooperation()
        {
            Cooperation cooperation = CreatePendingCooperation();
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }

        public static Cooperation CreateDoneCooperation()
        {
            Cooperation cooperation = CreateConfirmedCooperation();
            cooperation.MarkAsDone(DateTime.UtcNow);
            return cooperation;
        }

        public static Cooperation CreateCompletedCooperation()
        {
            Cooperation cooperation = CreateDoneCooperation();
            cooperation.Complete(DateTime.UtcNow);
            return cooperation;
        }
    }
}
