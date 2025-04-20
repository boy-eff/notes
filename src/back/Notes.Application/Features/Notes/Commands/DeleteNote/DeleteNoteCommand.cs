using MediatR;
using Notes.Application.Common.CQRS.Commands.Delete;
using Notes.Application.Common.Interfaces;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.DeleteNote;

/// <summary>
/// Команда удаления записки.
/// </summary>
public record DeleteNoteCommand : DeleteCommand<Note>
{
    /// <summary>
    /// ID записки.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Обработчик команды удаления записки.
    /// </summary>
    private class Handler(IRepository<Note> noteRepository) : DeleteCommandHandler<Note, DeleteNoteCommand>(noteRepository)
    {
    }
} 