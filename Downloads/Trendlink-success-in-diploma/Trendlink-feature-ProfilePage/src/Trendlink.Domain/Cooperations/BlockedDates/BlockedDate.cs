using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Cooperations.BlockedDates
{
    public sealed class BlockedDate : Entity<BlockedDateId>
    {
        private BlockedDate() { }

        public BlockedDate(UserId userId, DateOnly date)
            : base(BlockedDateId.New())
        {
            this.UserId = userId;
            this.Date = date;
        }

        public UserId UserId { get; private set; }

        public User User { get; init; }

        public DateOnly Date { get; private set; }
    }
}
