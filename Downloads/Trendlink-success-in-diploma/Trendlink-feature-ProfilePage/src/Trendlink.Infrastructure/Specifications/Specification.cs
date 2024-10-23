using System.Linq.Expressions;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Specifications
{
    public abstract class Specification<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        protected Specification(Expression<Func<TEntity, bool>>? criteria)
        {
            this.Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
            this.IncludeExpressions.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
            this.OrderByExpression = orderByExpression;

        protected void AddOrderDescendingBy(
            Expression<Func<TEntity, object>> orderByDescendingExpression
        ) => this.OrderByDescendingExpression = orderByDescendingExpression;

        public static Specification<TEntity, TEntityId> operator &(
            Specification<TEntity, TEntityId> left,
            Specification<TEntity, TEntityId> right
        )
        {
            Expression<Func<TEntity, bool>>? combinedCriteria =
                left.Criteria is not null && right.Criteria is not null
                    ? Expression.Lambda<Func<TEntity, bool>>(
                        Expression.AndAlso(
                            left.Criteria.Body,
                            Expression.Invoke(right.Criteria, left.Criteria.Parameters)
                        ),
                        left.Criteria.Parameters
                    )
                    : left.Criteria ?? right.Criteria;

            return new CombinedSpecification<TEntity, TEntityId>(combinedCriteria, left, right);
        }

        public static Specification<TEntity, TEntityId> operator |(
            Specification<TEntity, TEntityId> left,
            Specification<TEntity, TEntityId> right
        )
        {
            Expression<Func<TEntity, bool>>? combinedCriteria =
                left.Criteria is not null && right.Criteria is not null
                    ? Expression.Lambda<Func<TEntity, bool>>(
                        Expression.OrElse(
                            left.Criteria.Body,
                            Expression.Invoke(right.Criteria, left.Criteria.Parameters)
                        ),
                        left.Criteria.Parameters
                    )
                    : left.Criteria ?? right.Criteria;

            return new CombinedSpecification<TEntity, TEntityId>(combinedCriteria, left, right);
        }

        public static Specification<TEntity, TEntityId> operator !(
            Specification<TEntity, TEntityId> spec
        )
        {
            Expression<Func<TEntity, bool>>? notCriteria = spec.Criteria is not null
                ? Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Not(spec.Criteria.Body),
                    spec.Criteria.Parameters
                )
                : null;

            return new CombinedSpecification<TEntity, TEntityId>(notCriteria, spec);
        }
    }
}
