using Roots.Framework.Persistence.Repository;

namespace Roots.Framework.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, new();
    Task<int> SaveChangesAsync();
}