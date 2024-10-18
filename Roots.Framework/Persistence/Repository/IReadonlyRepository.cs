using System.Linq.Expressions;

namespace Roots.Framework.Persistence.Repository;

public interface IReadonlyRepository<T> where T : class, new()
{
    ICollection<T> GetBy(int index, int offset);
    Task<ICollection<T>> GetByAsync(int index, int offset, CancellationToken token);
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken token);
}