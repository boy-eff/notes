using MediatR;
using Notes.Application.Common.Interfaces;

namespace Notes.Application.Common.CQRS.Commands.Delete;

/// <summary>
/// Базовый обработчик команды удаления сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public abstract class DeleteCommandHandler<TEntity, TCommand>(IRepository<TEntity> repository) 
    : IRequestHandler<TCommand> 
    where TEntity : class
    where TCommand : DeleteCommand<TEntity>
{
    /// <inheritdoc />
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id);
        await repository.DeleteAsync(request.Id);
    }
} 