using MediatR;
using Notes.Application.Common.Interfaces;

namespace Notes.Application.Common.CQRS.Commands.Delete;

/// <summary>
/// Базовый обработчик команды удаления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public abstract class DeleteCommandHandler<TEntity, TId, TCommand>(IRepository<TEntity, TId> repository) 
    : IRequestHandler<TCommand> 
    where TEntity : class
    where TCommand : DeleteCommand<TEntity, TId>
{
    /// <inheritdoc />
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id);
        await repository.DeleteAsync(request.Id);
    }
} 