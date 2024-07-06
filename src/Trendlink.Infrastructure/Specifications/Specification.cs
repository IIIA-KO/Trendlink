using System.ComponentModel;
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
    }
}
