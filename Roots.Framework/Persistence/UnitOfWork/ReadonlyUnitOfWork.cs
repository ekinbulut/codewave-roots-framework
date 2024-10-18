using Microsoft.EntityFrameworkCore;
using Roots.Framework.Persistence.Repository;

namespace Roots.Framework.Persistence.UnitOfWork;

public class ReadonlyUnitOfWork : IReadonlyUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public ReadonlyUnitOfWork(DbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IReadonlyRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IReadonlyRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new BaseReadonlyRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}