using MediatR;
using MongoDB.Bson;

namespace Notes.Application.Common.CQRS.Queries.GetAll;

/// <summary>
/// Базовый класс запроса получения всех сущностей.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
public record GetAllQuery<TEntity, TDto>(WithPaginationParameters WithPaginationParameters) 
    : WithPaginationParameters(WithPaginationParameters.PageSize, WithPaginationParameters.PageNumber, WithPaginationParameters.LastId), 
        IRequest<IEnumerable<TDto>> where TEntity : class; 