using MediatR;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.Users.Instagarm.LinkInstagram
{
    internal sealed class InstagramAccountLinkedDomainEventHandler
        : INotificationHandler<InstagramAccountLinkedDomainEvent>
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstagramAccountLinkedDomainEventHandler(
            IUserTokenRepository userTokenRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userTokenRepository = userTokenRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            InstagramAccountLinkedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            Result<UserToken> userTokenResult = UserToken.Create(
                notification.UserId,
                notification.FacebookAccessToken,
                notification.ExpiresAt
            );
            if (userTokenResult.IsFailure)
            {
                return;
            }

            this._userTokenRepository.Add(userTokenResult.Value);
            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
