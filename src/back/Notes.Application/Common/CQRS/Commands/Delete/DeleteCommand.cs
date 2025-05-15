using MediatR;

namespace Notes.Application.Common.CQRS.Commands.Delete;

/// <summary>
/// Базовый класс команды удаления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности</typeparam>
public record DeleteCommand<TEntity, TId> : IRequest where TEntity : class
{
    /// <summary>
    /// ID сущности.
    /// </summary>
    public TId Id { get; init; }
} 