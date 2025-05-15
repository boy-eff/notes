using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;

namespace Notes.Application.Common.CQRS.Commands.Update;

/// <summary>
/// Базовый обработчик команды обновления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
/// <typeparam name="TCommand"></typeparam>
public abstract class UpdateCommandHandler<TEntity, TId, TDto, TCommand>(IRepository<TEntity, TId> repository, IMapperService mapper) 
    : IRequestHandler<TCommand> 
    where TEntity : class 
    where TDto : class
    where TCommand : UpdateCommand<TEntity, TId, TDto>
{
    /// <inheritdoc />
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            return;
        }

        mapper.Map(request.Data, entity);
        await repository.UpdateAsync(entity);
    }
} 