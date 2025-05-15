using MediatR;

namespace Notes.Application.Common.CQRS.Commands.Update;

/// <summary>
/// Базовый класс команды обновления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
public record UpdateCommand<TEntity, TId, TDto> : IRequest
    where TEntity : class 
    where TDto : class
{
    /// <summary>
    /// ID сущности.
    /// </summary>
    public TId Id { get; init; }

    /// <summary>
    /// Данные для обновления сущности.
    /// </summary>
    public TDto Data { get; init; } = null!;
} 