using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Data.Entities;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluater<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> specs) {
            var query = InputQuery;
            if(specs.Criteria is not null)
                query = query.Where(specs.Criteria);

            query = specs.Includes.Aggregate(query,(current,includeExpression) => current.Include(includeExpression));

            return query;
        
        }
    }
}
