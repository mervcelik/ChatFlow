using Core.Persistence.Entities;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public interface IRepository<T> where T : class,IEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool includeDeleted = false, CancellationToken ct = default);

    Task<Paginate<T>> GetListAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null, bool includeDeleted = false, CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task UpdateFieldAsync(T entity, Expression<Func<T, object>> field, object value, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
    Task SoftDeleteAsync(T entity, CancellationToken ct = default);
}
