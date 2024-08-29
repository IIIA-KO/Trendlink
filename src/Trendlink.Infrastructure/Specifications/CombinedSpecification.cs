using System.Linq.Expressions;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Specifications
{
    internal class CombinedSpecification<TEntity, TEntityId> : Specification<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        public CombinedSpecification(
            Expression<Func<TEntity, bool>>? criteria,
            params Specification<TEntity, TEntityId>[] specifications
        )
            : base(criteria)
        {
            foreach (Specification<TEntity, TEntityId> spec in specifications)
            {
                this.IncludeExpressions.AddRange(spec.IncludeExpressions);

                if (spec.OrderByExpression != null)
                {
                    this.AddOrderBy(spec.OrderByExpression);
                }

                if (spec.OrderByDescendingExpression != null)
                {
                    this.AddOrderDescendingBy(spec.OrderByDescendingExpression);
                }
            }
        }
    }
}
