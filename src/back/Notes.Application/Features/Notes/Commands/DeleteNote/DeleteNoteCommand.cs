using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.CQRS.Commands.Delete;
using Notes.Application.Common.Interfaces;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.DeleteNote;

/// <summary>
/// Команда удаления записки.
/// </summary>
public record DeleteNoteCommand : DeleteCommand<Note, ObjectId>
{
    /// <summary>
    /// Обработчик команды удаления записки.
    /// </summary>
    private class Handler(IRepository<Note, ObjectId> noteRepository) : DeleteCommandHandler<Note, ObjectId, DeleteNoteCommand>(noteRepository)
    {
    }
} 