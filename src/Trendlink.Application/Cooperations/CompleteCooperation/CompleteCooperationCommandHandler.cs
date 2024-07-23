﻿using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.CompleteCooperation
{
    internal sealed class CompleteCooperationCommandHandler
        : ICommandHandler<CompleteCooperationCommand>
    {
        private readonly ICooperationRepository _cooperationRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteCooperationCommandHandler(
            ICooperationRepository cooperationRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CompleteCooperationCommand request,
            CancellationToken cancellationToken
        )
        {
            Cooperation? cooperation = await this._cooperationRepository.GetByIdAsync(
                request.CooperationId,
                cancellationToken
            );
            if (cooperation is null)
            {
                return Result.Failure(CooperationErrors.NotFound);
            }

            Result result = cooperation.Complete(this._dateTimeProvider.UtcNow);
            if (result.IsFailure)
            {
                return result;
            }

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
