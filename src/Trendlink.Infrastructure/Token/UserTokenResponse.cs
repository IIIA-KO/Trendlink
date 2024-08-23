namespace Trendlink.Infrastructure.Token
{
    internal sealed record UserTokenResponse(Guid Id, string AccessToken)
    {
        public UserTokenResponse() : this(Guid.Empty, string.Empty) {  }
    }
}
