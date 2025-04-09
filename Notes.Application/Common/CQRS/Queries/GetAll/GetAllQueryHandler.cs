using System.Collections.Generic;
using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;

namespace Notes.Application.Common.CQRS.Queries.GetAll;

/// <summary>
/// Базовый обработчик запроса получения всех сущностей.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public abstract class GetAllQueryHandler<TEntity, TDto, TCommand>(IRepository<TEntity> repository, IMapperService mapper) 
    : IRequestHandler<TCommand, IEnumerable<TDto>> 
    where TEntity : class
    where TCommand : GetAllQuery<TEntity, TDto>
{
    /// <inheritdoc />
    public async Task<IEnumerable<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(x => mapper.Map<TEntity, TDto>(x));
    }
} 