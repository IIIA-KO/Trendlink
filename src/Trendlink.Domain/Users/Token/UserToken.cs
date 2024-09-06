using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.Token
{
    public sealed class UserToken
    {
        private UserToken() { }

        private UserToken(UserId userId, string accessToken, DateTimeOffset expiresAtUtc)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.AccessToken = accessToken;
            this.ExpiresAtUtc = expiresAtUtc;
        }

        public Guid Id { get; init; }

        public UserId UserId { get; init; }

        public User? User { get; init; }

        public string AccessToken { get; init; }

        public DateTime? LastCheckedOnUtc { get; init; }

        public DateTimeOffset ExpiresAtUtc { get; init; }

        public string? Error { get; init; }

        public static Result<UserToken> Create(
            UserId userId,
            string accessToken,
            DateTimeOffset expiresAtUtc
        )
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return Result.Failure<UserToken>(UserTokenErrors.AccessTokenInvalid);
            }

            return new UserToken(userId, accessToken, expiresAtUtc);
        }
    }
}
