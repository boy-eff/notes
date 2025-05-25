using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.CQRS.Commands.Delete;
using Notes.Application.Common.Interfaces;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.DeleteNote;

/// <summary>
/// Команда удаления записки.
/// </summary>
public record DeleteNoteCommand : DeleteCommand<NoteBase, ObjectId>
{
    /// <summary>
    /// Обработчик команды удаления записки.
    /// </summary>
    private class Handler(IRepository<NoteBase, ObjectId> noteRepository) : DeleteCommandHandler<NoteBase, ObjectId, DeleteNoteCommand>(noteRepository)
    {
    }
} 