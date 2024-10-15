namespace Trendlink.Application.Abstractions.Emails
{
    public interface IEmailService
    {
        Task SendAsync(
            Domain.Users.Email recipient,
            string subject,
            string body,
            bool isHtml = false
        );
    }
}
