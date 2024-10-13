using System.Data;
using Quartz;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Infrastructure.BackgroundJobs.EmailVerificationTokens
{
    [DisallowConcurrentExecution]
    internal sealed class DeleteExpiredEmailVerificationTokensJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpiredEmailVerificationTokensJob(
            ApplicationDbContext dbContext,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._dbContext = dbContext;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            IQueryable<EmailVerificationToken> verificationTokens = this
                ._dbContext.Set<EmailVerificationToken>()
                .Where(token => token.ExpiresAtUtc > this._dateTimeProvider.UtcNow);

            this._dbContext.Set<EmailVerificationToken>().RemoveRange(verificationTokens);

            await this._unitOfWork.SaveChangesAsync();
        }
    }
}
