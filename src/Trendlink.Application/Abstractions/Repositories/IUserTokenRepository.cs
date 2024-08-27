using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IUserTokenRepository
    {
        Task<UserToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<UserToken?> GetByUserIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default
        );

        void Add(UserToken userToken);

        void Remove(UserToken userToken);
    }
}
