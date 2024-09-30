using System.Linq.Expressions;
namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        protected void AddInclude(Expression<Func<T, object>> IncludeExpression) 
           => Includes.Add(IncludeExpression);
        public Expression<Func<T, object>> OrderBy { get;private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy=orderByExpression;
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
            => OrderByDesc = orderByDescExpression;

    }
}
