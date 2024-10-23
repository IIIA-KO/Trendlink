using Trendlink.Domain.Users;

namespace Trendlink.Infrastructure.Specifications.Users
{
    internal sealed class UserByIdWithTokenSpecification : Specification<User, UserId>
    {
        public UserByIdWithTokenSpecification(UserId userId)
            : base(user => user.Id == userId)
        {
            this.AddInclude(user => user.Token);
        }
    }
}
