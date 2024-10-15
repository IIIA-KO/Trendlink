using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.VerifyEmail
{
    internal sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand>
    {
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailCommandHandler(
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            VerifyEmailCommand request,
            CancellationToken cancellationToken
        )
        {
            EmailVerificationToken? token =
                await this._emailVerificationTokenRepository.GetByIdWithUser(
                    new EmailVerificationTokenId(request.Token),
                    cancellationToken
                );
            if (
                token is null
                || token.ExpiresAtUtc < this._dateTimeProvider.UtcNow
                || token.User.EmailVerified
            )
            {
                return Result.Failure(EmailVerificationTokenErrors.NotFound);
            }

            token.User.VerifyEmail(token);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
