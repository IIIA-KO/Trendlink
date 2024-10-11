using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Infrastructure.Emails
{
    internal sealed class EmailVerificationLinkFactory : IEmailVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public EmailVerificationLinkFactory(
            IHttpContextAccessor contextAccessor,
            LinkGenerator linkGenerator
        )
        {
            this._contextAccessor = contextAccessor;
            this._linkGenerator = linkGenerator;
        }

        public string Create(EmailVerificationToken emailVerificationToken)
        {
            string? verificationLink = this._linkGenerator.GetUriByName(
                this._contextAccessor.HttpContext!,
                "VerifyEmail",
                new { token = emailVerificationToken.Id.Value }
            );

            return verificationLink
                ?? throw new Exception("Could not create email verification link");
        }
    }
}
