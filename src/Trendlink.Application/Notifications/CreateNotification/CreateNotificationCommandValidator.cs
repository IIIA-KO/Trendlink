using FluentValidation;
using Trendlink.Domain.Notifications;

namespace Trendlink.Application.Notifications.CreateNotification
{
    internal sealed class CreateNotificationCommandValidator
        : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            this.RuleFor(c => c.NotificationType).IsInEnum();

            this.RuleFor(c => c.Title).NotEmpty().WithMessage("The title is required.");

            this.RuleFor(c => c.Message).NotEmpty().WithMessage("The message is required.");
        }
    }
}
