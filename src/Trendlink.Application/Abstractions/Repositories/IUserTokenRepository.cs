using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IUserTokenRepository
    {
        Task<UserToken?> GetByIdAsync(Guid id);

        void Add(UserToken userToken);

        void Update(UserToken userToken);
    }
}
