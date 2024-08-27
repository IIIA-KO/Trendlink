using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class UserTokenRepository : IUserTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserTokenRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<UserToken?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                ._dbContext.Set<UserToken>()
                .FirstOrDefaultAsync(userToken => userToken.Id == id, cancellationToken);
        }

        public async Task<UserToken?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                ._dbContext.Set<UserToken>()
                .FirstOrDefaultAsync<UserToken>(
                    userToken => userToken.UserId == userId,
                    cancellationToken
                );
        }

        public void Add(UserToken userToken)
        {
            this._dbContext.Add(userToken);
        }

        public void Remove(UserToken userToken)
        {
            this._dbContext.Remove(userToken);
        }
    }
}
