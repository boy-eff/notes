using MediatR;

namespace Notes.Application.Common.CQRS.Queries.GetById;

/// <summary>
/// Базовый класс запроса получения сущности по ID.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
public record GetByIdQuery<TEntity, TId, TDto> : IRequest<TDto?> where TEntity : class
{
    /// <summary>
    /// ID сущности.
    /// </summary>
    public TId Id { get; init; }
} 