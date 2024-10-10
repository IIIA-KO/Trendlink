using FluentEmail.Core;
using Trendlink.Application.Abstractions.Email;

namespace Trendlink.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            this._fluentEmail = fluentEmail;
        }

        public async Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
            await this._fluentEmail.To(recipient.Value).Subject(subject).Body(body).SendAsync();
        }
    }
}
