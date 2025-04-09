using MediatR;

namespace Notes.Application.Common.CQRS.Commands.Create;

/// <summary>
/// Базовый класс команды создания сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
public record CreateCommand<TEntity, TDto> : IRequest
    where TEntity : class 
    where TDto : class
{
    /// <summary>
    /// Данные для создания сущности.
    /// </summary>
    public TDto Data { get; init; } = null!;
}