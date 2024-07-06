using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TEntityId>(
            IQueryable<TEntity> inputQueryable,
            Specification<TEntity, TEntityId> specification
        )
            where TEntity : Entity<TEntityId>
            where TEntityId : class
        {
            IQueryable<TEntity> queryable = inputQueryable;

            if (specification.Criteria is not null)
            {
                queryable = queryable.Where(specification.Criteria);
            }

            _ = specification.IncludeExpressions.Aggregate(
                queryable,
                (current, includeExpression) => current.Include(includeExpression)
            );

            if (specification.OrderByExpression is not null)
            {
                queryable = queryable.OrderBy(specification.OrderByExpression);
            }
            else if (specification.OrderByDescendingExpression is not null)
            {
                queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
            }

            return queryable;
        }
    }
}
