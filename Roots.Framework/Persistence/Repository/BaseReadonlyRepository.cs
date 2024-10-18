using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Roots.Framework.Persistence.Repository;

public class BaseReadonlyRepository<T> : IReadonlyRepository<T> where T: class, new()
{
    private readonly DbContext _dbContext;
    public BaseReadonlyRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<T> GetBy(int index, int offset)
    {
        return _dbContext.Set<T>().Skip(offset).Take(index).ToList();
    }

    public async Task<ICollection<T>> GetByAsync(int index, int offset, CancellationToken token)
    {
        return await _dbContext.Set<T>().Skip(offset).Take(index).AsNoTracking().ToListAsync(token);
    }
    
    public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate) 
    {
        return _dbContext.Set<T>().Where(predicate);
    }
    
    public async Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
    {
        return await _dbContext.Set<T>().AsQueryable().Where(predicate).AsNoTracking().ToListAsync(token);
    }
}
