using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithStateSpecification : Specification<User, UserId>
    {
        public UserByIdWithStateSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.State);
            this.AddInclude(user => user.State.Country);
        }
    }
}
