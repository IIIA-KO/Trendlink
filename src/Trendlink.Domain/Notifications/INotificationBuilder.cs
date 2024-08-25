using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Domain.Notifications
{
    public interface IExpectsType
    {
        IExpectsTitle WithType(NotificationType type);
    }

    public interface IExpectsTitle
    {
        IExpectsMessage WithTitle(string title);
    }

    public interface IExpectsMessage
    {
        IExpectsCreationDate WithMessage(string message);
    }

    public interface IExpectsCreationDate
    {
        INotificationBuilder CreatedOn(DateTime createdOnUtc);
    }

    public interface INotificationBuilder
        : IExpectsType,
            IExpectsTitle,
            IExpectsMessage,
            IExpectsCreationDate
    {
        Notification Build();
    }
}
