using Microsoft.Extensions.Configuration;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Infrastructure.Emails
{
    internal sealed class EmailVerificationLinkFactory : IEmailVerificationLinkFactory
    {
        private readonly IConfiguration _configuration;

        public EmailVerificationLinkFactory(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string Create(EmailVerificationToken emailVerificationToken)
        {
            string frontendUrl = this._configuration.GetValue<string>("FrontendUrl");

            return $"{frontendUrl}/verify-email?token={emailVerificationToken.Id.Value}";
        }
    }
}
