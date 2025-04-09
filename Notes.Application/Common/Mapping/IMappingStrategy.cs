namespace Notes.Application.Common.Mapping;

/// <summary>
/// Интерфейс для стратегий маппинга.
/// </summary>
/// <typeparam name="TSource">Тип исходного объекта.</typeparam>
/// <typeparam name="TTarget">Тип целевого объекта.</typeparam>
public interface IMappingStrategy<in TSource, TTarget>
{
    /// <summary>
    /// Выполняет маппинг объекта.
    /// </summary>
    /// <param name="source">Исходный объект.</param>
    /// <param name="target">Целевой объект.</param>
    /// <returns>Результат маппинга.</returns>
    TTarget Map(TSource source, TTarget? target = default);
}