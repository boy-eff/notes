using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;

namespace Notes.Application.Common.CQRS.Commands.Create;

/// <summary>
/// Базовый обработчик команды создания сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public abstract class CreateCommandHandler<TEntity, TId, TDto, TCommand>(IRepository<TEntity, TId> repository, IMapperService mapper) 
    : IRequestHandler<TCommand> 
    where TEntity : class 
    where TDto : class
    where TCommand : CreateCommand<TEntity, TDto>
{
    /// <inheritdoc />
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TDto, TEntity>(request.Data);
        await repository.CreateAsync(entity);
    }
}