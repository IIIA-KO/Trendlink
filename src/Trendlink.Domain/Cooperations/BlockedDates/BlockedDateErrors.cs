using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations.BlockedDates
{
    public static class BlockedDateErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "BlockedDate.NotFound",
                "The blocked date with the specified identifier was not found"
            );

        public static readonly Error AlreadyBlocked =
            new("BlockedDate.AlreadyBlocked", "Date is already blocked");

        public static readonly Error PastDate =
            new("BlockedDate.PastDate", "The date has already past");
    }
}
