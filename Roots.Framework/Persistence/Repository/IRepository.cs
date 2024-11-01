namespace Roots.Framework.Persistence.Repository;

public interface IRepository<T> where T: class, new()
{
    T Insert(T entity);
    Task<T> InsertAsync(T entity, CancellationToken token);
    int BulkInsert(ICollection<T> entities);
    Task<int> BulkInsertAsync(ICollection<T> entities, CancellationToken token);
    bool DeleteBy(Guid id);    
    Task<bool> DeleteByAsync(Guid id, CancellationToken token);    
    bool DeleteBy(T entity);
    Task<bool> DeleteByAsync(T entity, CancellationToken token);
    bool Update(T entity);
    Task<bool> UpdateAsync(T entity, CancellationToken token);
}