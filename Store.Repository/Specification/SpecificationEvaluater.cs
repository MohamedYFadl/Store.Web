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
                query = query.Where(specs.Criteria); // x=> x.TypeId ==3

            if(specs.OrderBy is not null)
                query = query.OrderBy(specs.OrderBy); //x=> x.Name

            if(specs.OrderByDesc is not null)
                query = query.OrderByDescending(specs.OrderByDesc);

            if(specs.IsPaginated)
                query= query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Includes.Aggregate(query,(current,includeExpression) => current.Include(includeExpression));

            return query;
        
        }
    }
}
