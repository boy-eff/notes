using MediatR;
using Notes.Application.Common.CQRS.Queries.GetById;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Queries.GetNoteById;

/// <summary>
/// Запрос на получение записки по ID.
/// </summary>
public record GetNoteByIdQuery : GetByIdQuery<Note, NoteDto>
{
    /// <summary>
    /// ID записки.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Обработчик запроса на получение записки по ID.
    /// </summary>
    private class Handler(IRepository<Note> repository, IMapperService mapper) 
        : GetByIdQueryHandler<Note, NoteDto, GetNoteByIdQuery>(repository, mapper)
    {
    } 
} 