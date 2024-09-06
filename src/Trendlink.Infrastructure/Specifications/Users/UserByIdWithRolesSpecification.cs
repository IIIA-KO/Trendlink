using Trendlink.Domain.Users;

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
