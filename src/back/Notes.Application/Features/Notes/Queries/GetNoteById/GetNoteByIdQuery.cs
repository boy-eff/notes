using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.CQRS.Queries.GetById;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Queries.GetNoteById;

/// <summary>
/// Запрос на получение записки по ID.
/// </summary>
public record GetNoteByIdQuery : GetByIdQuery<NoteBase, ObjectId, NoteBaseDto>
{
    /// <summary>
    /// Обработчик запроса на получение записки по ID.
    /// </summary>
    private class Handler(IRepository<NoteBase, ObjectId> repository, IMapperService mapper) 
        : GetByIdQueryHandler<NoteBase, ObjectId, NoteBaseDto, GetNoteByIdQuery>(repository, mapper)
    {
    } 
} 