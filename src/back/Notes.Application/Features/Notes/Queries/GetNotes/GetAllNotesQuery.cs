using MongoDB.Bson;
using Notes.Application.Common.CQRS.Queries.GetAll;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Notes.Dto;
using Notes.Application.Features.Notes.Specifications;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Queries.GetNotes;

/// <summary>
/// Запрос на получение всех записок.
/// </summary>
public record GetAllNotesQuery(GetAllNotesQueryParameters Parameters) : GetAllQuery<NoteBase, NoteBaseDto>(Parameters)
{
    /// <summary>
    /// Обработчик запроса на получение всех записок.
    /// </summary>
    private class GetAllNotesQueryHandler(IRepository<NoteBase, ObjectId> repository, IMapperService mapper) 
        : GetAllQueryHandler<NoteBase, ObjectId, NoteBaseDto, GetAllNotesQuery>(repository, mapper)
    {
        public override IRepositorySpecification<NoteBase> ConstructSpecification(GetAllNotesQuery request)
        {
            NoteType? noteType = null;
            if (request.Parameters.NoteType is not null)
            {
                noteType = NoteType.GetByName(request.Parameters.NoteType);
            }

            return new NoteSpecification(request.PageSize, request.PageNumber, request.LastId, noteType);
        }
    } 
}