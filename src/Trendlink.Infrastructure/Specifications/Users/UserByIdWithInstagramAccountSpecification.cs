using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithInstagramAccountSpecification : Specification<User, UserId>
    {
        public UserByIdWithInstagramAccountSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.InstagramAccount);
        }
    }
}
