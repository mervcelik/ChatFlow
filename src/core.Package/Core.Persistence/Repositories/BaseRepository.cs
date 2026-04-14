using Core.Persistence.Attributes;
using Core.Persistence.Entities;
using Core.Persistence.Paging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class,IEntity
{
    protected readonly IMongoCollection<T> _collection;

    protected BaseRepository(IMongoDatabase database)
    {
        var collectionName = typeof(T)
    .GetCustomAttribute<BsonCollectionAttribute>()
    ?.CollectionName ?? typeof(T).Name.ToLower();
        _collection = database.GetCollection<T>(collectionName);
    }

    /// Helper method: Soft delete filter'ı ekle (IsDeleted = false)
    protected FilterDefinition<T> ApplySoftDeleteFilter(FilterDefinition<T>? filter = null, bool includeDeleted = false)
    {
        if (includeDeleted)
            return filter ?? Builders<T>.Filter.Empty;

        var softDeleteFilter = Builders<T>.Filter.Eq("IsDeleted", false);

        if (filter == null)
            return softDeleteFilter;

        return Builders<T>.Filter.And(filter, softDeleteFilter);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool includeDeleted = false, CancellationToken ct = default)
    {
        var mongoFilter = filter != null
            ? Builders<T>.Filter.Where(filter)
            : Builders<T>.Filter.Empty;

        var finalFilter = ApplySoftDeleteFilter(mongoFilter, includeDeleted);
        return await _collection.Find(finalFilter).FirstOrDefaultAsync(ct);
    }

    public async Task<Paginate<T>> GetListAsync(
        int page,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        bool includeDeleted = false,
        CancellationToken ct = default)
    {
        var mongoFilter = filter != null
            ? Builders<T>.Filter.Where(filter)
            : Builders<T>.Filter.Empty;

        var finalFilter = ApplySoftDeleteFilter(mongoFilter, includeDeleted);

        var totalCount = await _collection.CountDocumentsAsync(finalFilter, null, ct);

        var items = await _collection
            .Find(finalFilter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(ct);

        return new Paginate<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
        };
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        entity.CreatedDate = DateTime.Now;
        await _collection.InsertOneAsync(entity, null, ct);
    }


    public async Task UpdateAsync( T entity, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: ct);
    }
    public async Task UpdateFieldAsync(T entity,
    Expression<Func<T, object>> field,
    object value,
    CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.Id);
        var update = Builders<T>.Update.Set(field, value);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
    }

    public async Task SoftDeleteAsync(T entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(entity, ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.Id);
        await _collection.DeleteOneAsync(filter, ct);
    }
}
