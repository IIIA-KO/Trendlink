using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public override async Task<User?> GetByIdAsync(
            UserId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<User>()
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
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
            return await this.dbContext.Set<User>()
                .AsNoTracking()
                .AnyAsync(user => user.Email == email);
        }
    }
}
