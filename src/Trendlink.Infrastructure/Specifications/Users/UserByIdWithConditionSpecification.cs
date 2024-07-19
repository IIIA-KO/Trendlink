using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithConditionSpecification : Specification<User, UserId>
    {
        public UserByIdWithConditionSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.Condition);
        }
    }
}
