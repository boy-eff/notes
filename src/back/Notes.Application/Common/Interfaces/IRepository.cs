using System.Linq.Expressions;

namespace Notes.Application.Common.Interfaces;

/// <summary>
/// Интерфейс репозитория.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
public interface IRepository<TEntity, TId> where TEntity : class
{
    /// <summary>
    /// Получить все сущности.
    /// </summary>
    /// <returns>Коллекция сущностей.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Получить сущность по ID.
    /// </summary>
    /// <param name="id">ID сущности.</param>
    /// <returns>Сущность.</returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Найти сущности по условию.
    /// </summary>
    /// <param name="filterExpression">Условие фильтрации.</param>
    /// <returns>Коллекция сущностей.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filterExpression);

    /// <summary>
    /// Создать сущность.
    /// </summary>
    /// <param name="entity">Сущность для создания.</param>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id">ID сущности для удаления.</param>
    Task DeleteAsync(TId id);
} 