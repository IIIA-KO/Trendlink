using Microsoft.EntityFrameworkCore;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        protected readonly ApplicationDbContext dbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity?> GetByIdAsync(
            TEntityId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            this.dbContext.Add(entity);
        }
    }
}
