namespace Notes.Application.Common.Interfaces;

/// <summary>
/// Сервис мапинга.
/// </summary>
public interface IMapperService
{
    /// <summary>
    /// Метод для мапинга сущностей.
    /// </summary>
    /// <param name="source">Объект, с которого необходим мапинг.</param>
    /// <param name="target">Объект мапинга.</param>
    /// <typeparam name="TSource">Тип-источник.</typeparam>
    /// <typeparam name="TTarget">Тип-получатель.</typeparam>
    /// <returns></returns>
    TTarget Map<TSource, TTarget>(TSource source, TTarget? target = default);
}