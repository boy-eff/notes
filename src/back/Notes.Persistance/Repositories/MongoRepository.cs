using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Notes.Application.Common.Interfaces;
using Notes.Persistance.Configuration;

namespace Notes.Persistance.Repositories;

/// <summary>
/// Реализация репозитория MongoDB.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
public class MongoRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MongoRepository{TEntity, TId}"/>.
    /// </summary>
    /// <param name="configuration">Конфигурация MongoDB.</param>
    public MongoRepository(IOptions<MongoDbConfiguration> configuration)
    {
        var client = new MongoClient(configuration.Value.ConnectionString);
        var database = client.GetDatabase(configuration.Value.DatabaseName);
        var collectionName = typeof(TEntity).Name.ToLower();
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(IRepositorySpecification<TEntity> specification)
    {
        var query = _collection.AsQueryable();
        query = specification.BuildFilters(query);
        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }
} 