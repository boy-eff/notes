using MongoDB.Bson;
using Notes.Application.Common.CQRS;
using Notes.Application.Common.Extensions;
using Notes.Application.Common.Interfaces;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Specifications;

public record NoteSpecification(int PageSize, int? PageNumber, ObjectId? LastId, NoteType? NoteType) 
    : WithPaginationParameters(PageSize, PageNumber, LastId), IRepositorySpecification<NoteBase>
{
    /// <summary>
    /// Тип заметки.
    /// </summary>
    public NoteType? NoteType { get; set; } = NoteType;

    public IQueryable<NoteBase> BuildFilters(IQueryable<NoteBase> entities)
    {
        entities = entities.ApplyPagination(this);
        if (NoteType != null)
        {
            entities = entities.Where(x => x.Type == NoteType.Name);
        }
        
        return entities;
    }
}