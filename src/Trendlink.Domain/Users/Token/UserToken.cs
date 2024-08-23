using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.Token
{
    public sealed class UserToken
    {
        private UserToken() { }

        private UserToken(string accessToken, UserId userId)
        {
            this.Id = Guid.NewGuid();
            this.AccessToken = accessToken;
            this.UserId = userId;
        }

        public Guid Id { get; init; }

        public UserId UserId { get; init; }

        public User? User { get; init; }

        public string AccessToken { get; init; }

        public DateTime? LastCheckedOnUtc { get; init; }

        public static Result<UserToken> Create(string accessToken, UserId userId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Result.Failure<UserToken>(UserTokenErrors.AccessTokenInvalid);
            }

            return new UserToken(accessToken, userId);
        }
    }
}
