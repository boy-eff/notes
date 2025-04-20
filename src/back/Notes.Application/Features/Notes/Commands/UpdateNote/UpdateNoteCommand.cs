using MediatR;
using Notes.Application.Common.CQRS.Commands.Update;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.UpdateNote;

/// <summary>
/// Команда обновления записки.
/// </summary>
public record UpdateNoteCommand : UpdateCommand<Note, NoteDto>
{
    /// <summary>
    /// Обработчик команды обновления записки.
    /// </summary>
    private class Handler(IRepository<Note> noteRepository, IMapperService mapperService)
        : UpdateCommandHandler<Note, NoteDto, UpdateNoteCommand>(noteRepository, mapperService);
} 