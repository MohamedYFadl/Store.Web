using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

        public  void DeleteAsync(TEntity entity)
        =>  _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
        => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();



        public async Task<TEntity> GetByIdAsync(Tkey? id)
            => await _context.Set<TEntity>().FindAsync(id);
        public void UpdateAsync(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
        public async Task<TEntity> GetWithSpecsByIdAsync(ISpecification<TEntity> specs)
        => await ApplySpecification(specs).FirstOrDefaultAsync();
        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecsAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).ToListAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
            =>  SpecificationEvaluater<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), specs);
    }
}
