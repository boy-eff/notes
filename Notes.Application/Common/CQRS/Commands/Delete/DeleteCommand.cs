using MediatR;

namespace Notes.Application.Common.CQRS.Commands.Delete;

/// <summary>
/// Базовый класс команды удаления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public record DeleteCommand<TEntity> : IRequest where TEntity : class
{
    /// <summary>
    /// ID сущности.
    /// </summary>
    public int Id { get; init; }
} 