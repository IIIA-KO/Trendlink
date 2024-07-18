using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Notifications.CreateNotification
{
    internal sealed class CreateNotificationCommandValidator
        : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            this.RuleFor(c => c.NotificationType).IsInEnum();

            this.RuleFor(c => c.Title).NotNullOrEmpty();

            this.RuleFor(c => c.Title.Value).NotNullOrEmpty();

            this.RuleFor(c => c.Message).NotNullOrEmpty();

            this.RuleFor(c => c.Message.Value).NotNullOrEmpty();
        }
    }
}
