using Store.Data.Entities;

namespace Store.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity,Tkey> Repository<TEntity,Tkey>() where TEntity:BaseEntity<Tkey>;
        Task<int> CompleteAsync();
    }
}
