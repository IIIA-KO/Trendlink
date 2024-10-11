using FluentEmail.Core;
using Trendlink.Application.Abstractions.Emails;

namespace Trendlink.Infrastructure.Emails
{
    internal sealed class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            this._fluentEmail = fluentEmail;
        }

        public async Task SendAsync(
            Domain.Users.Email recipient,
            string subject,
            string body,
            bool isHtml = false
        )
        {
            await this
                ._fluentEmail.To(recipient.Value)
                .Subject(subject)
                .Body(body, isHtml)
                .SendAsync();
        }
    }
}
