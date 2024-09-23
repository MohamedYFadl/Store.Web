using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;

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
    }
}
