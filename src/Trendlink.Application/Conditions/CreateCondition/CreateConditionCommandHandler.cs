using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Conditions.CreateCondition
{
    internal sealed class CreateConditionCommandHandler
        : ICommandHandler<CreateConditionCommand, ConditionId>
    {
        private readonly IUserContext _userContext;
        private readonly IConditionRepository _conditionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateConditionCommandHandler(
            IUserContext userContext,
            IConditionRepository conditionRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userContext = userContext;
            this._conditionRepository = conditionRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<ConditionId>> Handle(
            CreateConditionCommand request,
            CancellationToken cancellationToken
        )
        {
            UserId userId = this._userContext.UserId;

            bool conditionExists = await this._conditionRepository.ExistsByUserId(
                userId,
                cancellationToken
            );
            if (conditionExists)
            {
                return Result.Failure<ConditionId>(ConditionErrors.Duplicate);
            }

            Result<Condition> result = Condition.Create(userId, request.Description);

            if (result.IsFailure)
            {
                return Result.Failure<ConditionId>(result.Error);
            }

            Condition condition = result.Value;

            this._conditionRepository.Add(condition);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return condition.Id;
        }
    }
}
