using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Abstractions.Emails
{
    public interface IEmailVerificationLinkFactory
    {
        string Create(EmailVerificationToken emailVerificationToken);
    }
}
