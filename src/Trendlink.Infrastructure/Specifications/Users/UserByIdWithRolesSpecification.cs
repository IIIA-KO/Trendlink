using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithRolesSpecification : Specification<User, UserId>
    {
        public UserByIdWithRolesSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.Roles);
        }
    }
}
