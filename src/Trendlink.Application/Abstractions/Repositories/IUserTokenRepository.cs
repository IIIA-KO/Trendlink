using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IUserTokenRepository
    {
        Task<UserToken?> GetByIdAsync(Guid id);

        void Add(UserToken userToken);

        void Update(UserToken userToken);
    }
}
