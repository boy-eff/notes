using MongoDB.Bson;
using Notes.Application.Common.CQRS.Queries.GetAll;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Queries.GetNotes;

/// <summary>
/// Запрос на получение всех записок.
/// </summary>
public record GetAllNotesQuery : GetAllQuery<Note, NoteDto>
{
    /// <summary>
    /// Обработчик запроса на получение всех записок.
    /// </summary>
    private class GetAllNotesQueryHandler(IRepository<Note, ObjectId> repository, IMapperService mapper) 
        : GetAllQueryHandler<Note, ObjectId, NoteDto, GetAllNotesQuery>(repository, mapper)
    {
    } 
}