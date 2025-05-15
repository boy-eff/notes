using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.CQRS.Commands.Update;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.UpdateNote;

/// <summary>
/// Команда обновления записки.
/// </summary>
public record UpdateNoteCommand : UpdateCommand<Note, ObjectId, UpdateNoteDto>
{
    /// <summary>
    /// Обработчик команды обновления записки.
    /// </summary>
    private class Handler(IRepository<Note, ObjectId> noteRepository, IMapperService mapperService)
        : UpdateCommandHandler<Note, ObjectId, UpdateNoteDto, UpdateNoteCommand>(noteRepository, mapperService);
} 