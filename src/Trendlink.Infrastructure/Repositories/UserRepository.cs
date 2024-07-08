using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure.Specifications.Users;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<User?> GetByIdWithRolesAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(new UserByIdWithRolesSpecification(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public override void Add(User user)
        {
            foreach (Role role in user.Roles)
            {
                this.dbContext.Attach(role);
            }

            this.dbContext.Add(user);
        }

        public async Task<bool> UserExists(Email email)
        {
            return await this
                .dbContext.Set<User>()
                .AsNoTracking()
                .AnyAsync(user => user.Email == email);
        }
    }
}
