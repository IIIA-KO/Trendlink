using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public override void Add(User user)
        {
            foreach (Role role in user.Roles)
            {
                this.dbContext.Attach(role);
            }

            this.dbContext.Add(user);
        }
    }
}
