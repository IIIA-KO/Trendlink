namespace Trendlink.Domain.Users.VerificationTokens
{
    public sealed record EmailVerificationTokenId(Guid Value)
    {
        public static EmailVerificationTokenId New() => new(Guid.NewGuid());
    }
}
