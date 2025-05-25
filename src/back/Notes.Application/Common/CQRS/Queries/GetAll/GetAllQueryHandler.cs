using System.Collections.Generic;
using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;

namespace Notes.Application.Common.CQRS.Queries.GetAll;

/// <summary>
/// Базовый обработчик запроса получения всех сущностей.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public abstract class GetAllQueryHandler<TEntity, TId, TDto, TCommand>(IRepository<TEntity, TId> repository, IMapperService mapper) 
    : IRequestHandler<TCommand, IEnumerable<TDto>> 
    where TEntity : class
    where TCommand : GetAllQuery<TEntity, TDto>
{
    /// <inheritdoc />
    public async Task<IEnumerable<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
        var specification = ConstructSpecification(request);
        var entities = await repository.GetAllAsync(specification);
        return entities.Select(x => mapper.Map<TEntity, TDto>(x));
    }
    
    public abstract IRepositorySpecification<TEntity> ConstructSpecification(TCommand request);
} 