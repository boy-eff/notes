using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;

namespace Notes.Application.Common.CQRS.Queries.GetById;

/// <summary>
/// Базовый обработчик запроса получения сущности по ID.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public class GetByIdQueryHandler<TEntity, TDto, TCommand>(IRepository<TEntity> repository, IMapperService mapper) 
    : IRequestHandler<TCommand, TDto?> 
    where TEntity : class
    where TCommand : GetByIdQuery<TEntity, TDto>
{
    /// <inheritdoc />
    public async Task<TDto?> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            return default;
        }

        return mapper.Map<TEntity, TDto>(entity);
    }
} 