using Microsoft.EntityFrameworkCore;

namespace Roots.Framework.Persistence.Repository;

public class BaseRepository<T> : IRepository<T> where T: class, new()
{
    private readonly DbContext _dbContext;
    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T Insert(T entity)
    {
        return _dbContext.Set<T>().Add(entity).Entity;
    }

    public async Task<T> InsertAsync(T entity, CancellationToken token)
    {
        var result = await _dbContext.Set<T>().AddAsync(entity, token);
        return result.Entity;
    }

    public int BulkInsert(ICollection<T> entities)
    {
        _dbContext.Set<T>().AddRange(entities);
        return _dbContext.SaveChanges(); 
    }

    public async Task<int> BulkInsertAsync(ICollection<T> entities, CancellationToken token)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, token);
        return await _dbContext.SaveChangesAsync(token);
    }

    public bool DeleteBy(Guid id)
    {
        _dbContext.Set<T>().Remove(_dbContext.Set<T>().Find(id));
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> DeleteByAsync(Guid id, CancellationToken token)
    {
        return await Task.FromResult(DeleteBy(id));
    }

    public bool DeleteBy(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> DeleteByAsync(T entity, CancellationToken token)
    {
        return await Task.FromResult(DeleteBy(entity));
    }
}