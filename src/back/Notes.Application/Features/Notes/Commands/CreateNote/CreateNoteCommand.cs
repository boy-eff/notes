using Notes.Application.Common.CQRS.Commands.Create;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.CreateNote;

/// <summary>
/// Команда создания записки.
/// </summary>
public record CreateNoteCommand : CreateCommand<Note, CreateNoteDto>
{
    /// <summary>
    /// Обработчик команды создания записки.
    /// </summary>
    private class Handler(IRepository<Note> repository, IMapperService mapper) 
        : CreateCommandHandler<Note, CreateNoteDto, CreateNoteCommand>(repository, mapper)
    {
    } 
} 