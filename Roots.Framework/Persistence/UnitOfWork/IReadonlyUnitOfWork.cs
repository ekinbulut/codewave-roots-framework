using Roots.Framework.Persistence.Repository;

namespace Roots.Framework.Persistence.UnitOfWork;

public interface IReadonlyUnitOfWork : IDisposable
{
    IReadonlyRepository<TEntity> Repository<TEntity>() where TEntity : class, new();
}