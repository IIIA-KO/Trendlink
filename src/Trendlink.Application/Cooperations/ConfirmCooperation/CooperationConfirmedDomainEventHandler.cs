﻿using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations.ConfirmCooperation
{
    internal sealed class CooperationConfirmedDomainEventHandler
        : INotificationHandler<CooperationConfirmedDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.CooperationConfirmed
        );

        private readonly ICooperationRepository _cooperationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationConfirmedDomainEventHandler(
            ICooperationRepository cooperationRepository,
            IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._userRepository = userRepository;
            this._advertisementRepository = advertisementRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CooperationConfirmedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            Cooperation? cooperation = await this._cooperationRepository.GetByIdAsync(
                notification.CooperationId,
                cancellationToken
            );
            if (cooperation is null)
            {
                return;
            }

            Advertisement? advertisement = await this._advertisementRepository.GetByIdAsync(
                cooperation.AdvertisementId,
                cancellationToken
            );
            if (advertisement is null)
            {
                return;
            }

            User? seller = await this._userRepository.GetByIdAsync(
                cooperation.SellerId,
                cancellationToken
            );
            if (seller is null)
            {
                return;
            }

            string cooperationConfirmedMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                advertisement.Name.Value,
                seller.FirstName.Value
            );

            Result<Notification> result = Notification.Create(
                cooperation.BuyerId,
                NotificationType.System,
                new Title("Cooperation Confirmed"),
                new Message(cooperationConfirmedMessage),
                this._dateTimeProvider.UtcNow
            );

            if (result.IsFailure)
            {
                return;
            }

            this._notificationRepository.Add(result.Value);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}