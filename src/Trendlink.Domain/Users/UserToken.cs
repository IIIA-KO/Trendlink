namespace Trendlink.Domain.Users
{
    public sealed class UserToken
    {
        public UserToken(string accessToken)
        {
            this.Id = Guid.NewGuid();
            this.AccessToken = accessToken;
        }

        public Guid Id { get; init; }

        public string AccessToken { get; init; }

        public DateTime? LastCheckedOnUtc { get; init; }
    }
}
